using DynamicJob.Abstractions;
using Hangfire.DynamicJob.Client.JobSettings;

namespace Hangfire.DynamicJob.Client
{
    public class DynamicJobClient : IDynamicJobClient
    {
        public void EnqueueBackgoundJob(BackgoundJobSettings settings, string arguments)
        {
            BackgroundJob.Enqueue<IJobWrapper>(j => j.Run(settings.Name));
        }

        public void AddOrUpdateRecurringJob(RecurringJobSettings settings, string arguments)
        {
            throw new System.NotImplementedException();
        }

        public void ScheduleJob(ScheduleJobSettings settings, string arguments)
        {
            throw new System.NotImplementedException();
        }
    }
}
