namespace Hangfire.DynamicJob.Core.JobSettings
{
    public class RecurringJobSettings : IJobSettings
    {
        public string CronExpression { get; set; }

        public string TimeZone { get; set; }

        public string Name { get; set; }

        public string Queue { get; set; } = "default";
    }
}
