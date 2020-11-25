using Cronos;
using System;
using System.Threading;
using System.Threading.Tasks;
using Webscan.Scheduler.Models;

namespace Webscan.Scheduler.Services
{
    public class ScheduledStatusCheckProducer : IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly StatusCheck _statusCheck;

        public ScheduledStatusCheckProducer(TimeZoneInfo timeZoneInfo, StatusCheck statusCheck)
        {
            _statusCheck = statusCheck ?? throw new ArgumentNullException($"{nameof(statusCheck)} cannot be null");
            if(!string.IsNullOrEmpty(statusCheck.CronExpression)) _expression = CronExpression.Parse(statusCheck.CronExpression);
            _timeZoneInfo = timeZoneInfo ?? throw new ArgumentNullException($"{nameof(TimeZoneInfo)} cannot be null");
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
            Console.WriteLine($"{_statusCheck.Name}:\n\tURL:{_statusCheck.Url}");
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
