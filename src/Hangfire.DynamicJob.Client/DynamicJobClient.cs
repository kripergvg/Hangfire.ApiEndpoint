using DynamicJob.Abstractions;
using Hangfire.DynamicJob.Client.JobSettings;

namespace Hangfire.DynamicJob.Client
{
    public class DynamicJobClient : IDynamicJobClient
    {
        public void EnqueueBackgoundJob(BackgoundJobSettings settings)
        {
            BackgroundJob.Enqueue<IJobWrapper>(j => j.Run(settings.Name));
        }

        public void AddOrUpdateRecurringJob(RecurringJobSettings settings)
        {
            throw new System.NotImplementedException();
        }

        public void ScheduleJob(ScheduleJobSettings settings)
        {
            throw new System.NotImplementedException();
        }
    }
}
