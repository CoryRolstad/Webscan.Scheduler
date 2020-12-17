using Confluent.Kafka;
using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Webscan.Scheduler.Models;
using Webscan.Scheduler.Models.Repository;

namespace Webscan.Scheduler.Services
{
    public class ScheduledStatusCheckProducer : IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly StatusCheck _statusCheck;
        private readonly IServiceProvider _serviceProvider; 

        public ScheduledStatusCheckProducer(TimeZoneInfo timeZoneInfo, StatusCheck statusCheck, IServiceProvider serviceProvider)
        {
            _statusCheck = statusCheck ?? throw new ArgumentNullException($"{nameof(statusCheck)} cannot be null");
            if(!string.IsNullOrEmpty(statusCheck.CronExpression)) _expression = CronExpression.Parse(statusCheck.CronExpression);
            _timeZoneInfo = timeZoneInfo ?? throw new ArgumentNullException($"{nameof(TimeZoneInfo)} cannot be null");
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException($"{nameof(_serviceProvider)} cannot be null");
        }

        public async Task ScheduleJob(CancellationToken cancellationToken)
        {
            //var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            DateTimeOffset? next = GetDateTimeOffset();
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken);
                }
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();  // reset and dispose timer
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWork(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken);    // reschedule next
                    }
                };
                _timer.Start();
            }
            await Task.CompletedTask;
        }

        public DateTimeOffset? GetDateTimeOffset()
        {
            if (_expression != null)
                return _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            else
                return DateTimeOffset.Now.AddSeconds(_statusCheck.QueryTimeInSeconds);
        }

        public virtual async Task DoWork(CancellationToken cancellationToken)
        {

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ILogger<Worker> _logger = scope.ServiceProvider.GetRequiredService<ILogger<Worker>>();
                _logger.LogInformation($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")}: {_statusCheck.Name} Added to Scheduler Queue(Topic: StatusCheck):\n\tURL:{_statusCheck.Url}");

                IOptions<KafkaSettings> kafkaSettings = scope.ServiceProvider.GetRequiredService<IOptions<KafkaSettings>>();

                var conf = new ProducerConfig
                {
                    BootstrapServers = kafkaSettings.Value.Broker,
                    ClientId = _statusCheck.Name
                };

                using (var p = new ProducerBuilder<Null, string>(conf).Build())
                {
                    _statusCheck.TimeScheduled = DateTime.Now; 
                    var response = await p.ProduceAsync(kafkaSettings.Value.SchedulerTopicName, new Message<Null, string> { Value = JsonConvert.SerializeObject(_statusCheck, Formatting.Indented) })
                        .ContinueWith(task => task.IsFaulted
                                ? $"error producing message: {task.Exception.Message}"
                                : $"produced to: {task.Result.TopicPartitionOffset}");

                    // wait for up to 10 seconds for any inflight messages to be delivered.
                    p.Flush(TimeSpan.FromSeconds(10));
                }
            }

        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
