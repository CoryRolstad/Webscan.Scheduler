using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Webscan.Scheduler.Models;
using Webscan.Scheduler.Models.Repository;

namespace Webscan.Scheduler.Services
{
    public class StatusCheckCronJob : CronJobService
    {
        private readonly ILogger<StatusCheckCronJob> _logger;
        private readonly IServiceProvider _serviceProvider;

        public StatusCheckCronJob(IScheduleConfig<StatusCheckCronJob> config, ILogger<StatusCheckCronJob> logger, IServiceProvider serviceProvider)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException($"{nameof(serviceProvider)} cannot be null");
            _logger = logger ?? throw new ArgumentNullException($"{nameof(serviceProvider)} cannot be null");
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StatusCheck Worker has started.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() =>
            {
                _logger.LogInformation("StatusCheck Worker shutting down");
            });

            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} StatusCheck is working.");

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IStatusCheckRepository<StatusCheck> statusCheckRepository = scope.ServiceProvider.GetRequiredService<IStatusCheckRepository<StatusCheck>>();
                IEnumerable<StatusCheck> statusChecks = statusCheckRepository.GetAll();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);


                var conf = new ProducerConfig { BootstrapServers = "127.0.0.1:9092" };

                Action<DeliveryReport<Null, string>> handler = r =>
                    Console.WriteLine(!r.Error.IsError
                        ? $"Delivered message to {r.TopicPartitionOffset}"
                        : $"Delivery Error: {r.Error.Reason}");

                using (var p = new ProducerBuilder<Null, string>(conf).Build())
                {
                    for (int i = 0; i < 100; ++i)
                    {
                        p.Produce("StatusCheck", new Message<Null, string> { Value = i.ToString() }, handler);
                    }

                    // wait for up to 10 seconds for any inflight messages to be delivered.
                    p.Flush(TimeSpan.FromSeconds(10));
                }


            }

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StatusCheck Worker has stopped.");
            return base.StopAsync(cancellationToken);
        }
    }
}
