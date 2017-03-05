using Hangfire.DynamicJob.Client.JobSettings;

namespace Hangfire.DynamicJob.Client
{
    public interface IDynamicJobClient
    {
        void EnqueueBackgoundJob(BackgoundJobSettings settings, string arguments);

        void AddOrUpdateRecurringJob(RecurringJobSettings settings, string arguments);

        void ScheduleJob(ScheduleJobSettings settings, string arguments);
    }
}
