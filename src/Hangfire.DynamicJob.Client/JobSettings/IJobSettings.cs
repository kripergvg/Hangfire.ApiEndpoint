namespace Hangfire.DynamicJob.Client.JobSettings
{
    interface IJobSettings
    {
        string Name { get; set; }

        string Queue { get; set; }
    }
}
