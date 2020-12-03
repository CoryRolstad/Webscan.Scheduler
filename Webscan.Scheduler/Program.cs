using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Webscan.Scheduler.datastore;
using Microsoft.Extensions.Configuration;
using Webscan.Scheduler.Models.Repository;
using Webscan.Scheduler.Models;

namespace Webscan.Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    
                    services.AddDbContext<WebscanContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("Webscan")));

                    services.Configure<KafkaSettings>(configuration.GetSection("KafkaSettings"));

                    services.AddScoped<IStatusCheckRepository<StatusCheck>, StatusCheckRepository>();
                    services.AddScoped<IUserRepository<User>, UserRepository>();

                    services.AddHostedService<Worker>();

                });
    }
}
