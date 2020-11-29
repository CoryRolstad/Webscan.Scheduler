using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Webscan.Scheduler.Helpers;
using Webscan.Scheduler.Models;
using Webscan.Scheduler.Models.Repository;
using Webscan.Scheduler.Services;

namespace Webscan.Scheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration config)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException($"{nameof(serviceProvider)} cannot be null");
            _config = config ?? throw new ArgumentNullException($"{nameof(config)} cannot be null");
            _logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} cannot be null");
        }

        public IEnumerable<StatusCheck> GetStatusChecks()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                _logger.LogInformation($"{DateTime.Now}: Retreiving StatusChecks From Database");
                IStatusCheckRepository<StatusCheck> statusCheckRepository = scope.ServiceProvider.GetRequiredService<IStatusCheckRepository<StatusCheck>>();
                return statusCheckRepository.GetAll();
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                _logger.LogInformation("TaskProcessingService shutting down");
            });

            try
            {

                IEnumerable<StatusCheck> statusChecks = GetStatusChecks();

                while (!stoppingToken.IsCancellationRequested)
                {
                    List<Task> taskList = new List<Task>();

                    foreach(StatusCheck statusCheck in statusChecks)
                    {
                        _logger.LogInformation($"Creating {statusCheck.Name} Instance of Scheduled Task");
                        ScheduledStatusCheckProducer scheduledTask = new ScheduledStatusCheckProducer(TimeZoneInfo.Local, statusCheck, _serviceProvider);
                        taskList.Add(scheduledTask.ScheduleJob(stoppingToken));
                    }

                    _logger.LogInformation("Main Thread Waiting");
                    await Task.Delay(-1, stoppingToken);
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
