#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Webscan.Scheduler/Webscan.Scheduler.csproj", "Webscan.Scheduler/"]
RUN dotnet restore "Webscan.Scheduler/Webscan.Scheduler.csproj"
COPY . .
WORKDIR "/src/Webscan.Scheduler"
RUN dotnet build "Webscan.Scheduler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webscan.Scheduler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

RUN export DEBIAN_FRONTEND=noninteractive && \
    apt-get update -y && \
    apt-get install -y iputils-ping && \
    apt-get install -y tzdata && \
    ln -fs /usr/share/zoneinfo/America/New_York /etc/localtime && \
    dpkg-reconfigure --frontend noninteractive tzdata

COPY --from=publish /app/publish .

# Do not run as root user
RUN chown -R www-data:www-data /app
USER www-data

ENTRYPOINT ["dotnet", "Webscan.Scheduler.dll"]