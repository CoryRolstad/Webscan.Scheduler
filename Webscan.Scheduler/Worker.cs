using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Webscan.Scheduler.Models;
using Webscan.Scheduler.Models.Repository;

namespace Webscan.Scheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException($"{nameof(serviceProvider)} cannot be null");
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                _logger.LogInformation("TaskProcessingService shutting down");
            });

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        IStatusCheckRepository<StatusCheck> statusCheckRepository = scope.ServiceProvider.GetRequiredService<IStatusCheckRepository<StatusCheck>>();
                        IEnumerable<StatusCheck> statusChecks = statusCheckRepository.GetAll();
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                        await Task.Delay(1000, stoppingToken);
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                //Swallow this since we expect this to occur when shutdown has been signaled.
                _logger.LogWarning(e, "A task/operation cancelled exception was caught.");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "An unhandled exception was thrown in the task processing background service.");
            }
            finally
            {
                _logger.LogCritical("The task processing background service is shutting down!");
                // TODO Send email to group to alert to an issue.
                //_hostApplicationLifetime.StopApplication(); //Should we shutdown the app or alert somehow?
            }
        }

    }
}
