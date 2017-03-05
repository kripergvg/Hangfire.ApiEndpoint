namespace Hangfire.DynamicJob.Core.JobSettings
{
    public class BackgoundJobSettings : IJobSettings
    {
        public string Name { get; set; }

        public string Queue { get; set; }
    }
}
