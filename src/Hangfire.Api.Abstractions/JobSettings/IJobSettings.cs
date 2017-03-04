namespace Hangfire.Api.Abstractions.JobSettings
{
    interface IJobSettings
    {
        string Name { get; set; }

        string Queue { get; set; }
    }
}
