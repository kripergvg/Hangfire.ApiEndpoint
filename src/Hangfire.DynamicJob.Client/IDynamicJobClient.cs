using Hangfire.DynamicJob.Client.JobSettings;

namespace Hangfire.DynamicJob.Client
{
    public interface IDynamicJobClient
    {
        void EnqueueBackgoundJob(BackgoundJobSettings settings);

        void AddOrUpdateRecurringJob(RecurringJobSettings settings);

        void ScheduleJob(ScheduleJobSettings settings);
    }
}
