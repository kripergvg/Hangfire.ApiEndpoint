using System;

namespace Hangfire.Api.Abstractions.JobSettings
{
    public class ScheduleJobSettings : IJobSettings
    {
        public TimeSpan Delay { get; set; }

        public DateTimeOffset EnqueueAt { get; set; }

        public string Name { get; set; }

        public string Queue { get; set; }
    }
}
