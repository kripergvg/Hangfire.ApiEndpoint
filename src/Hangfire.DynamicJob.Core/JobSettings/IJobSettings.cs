namespace Hangfire.DynamicJob.Core.JobSettings
{
    interface IJobSettings
    {
        string Name { get; set; }

        string Queue { get; set; }
    }
}
