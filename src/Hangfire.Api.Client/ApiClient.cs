using Hangfire.Api.Abstractions.JobSettings;
using DynamicJob;

namespace Hangfire.Api.Client
{
    public class ApiClient : IApiClient
    {
        public void EnqueueBackgoundJob(BackgoundJobSettings settings)
        {
            BackgroundJob.Enqueue(() => new Job(settings.Name).Run());
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
