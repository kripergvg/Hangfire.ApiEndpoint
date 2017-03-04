using System;

namespace Hangfire.ApiEndpoint.JobSettings
{
    public class ScheduleJobSettings
    {
        public TimeSpan Delay { get; set; }

        public DateTimeOffset EnqueueAt { get; set; }
    }
}
