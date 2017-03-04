using Hangfire.Api.Abstractions.JobSettings;
using DynamicJob;

namespace Hangfire.Api.Client
{
    public class ApiClient : IApiClient
    {
        public void EnqueueBackgoundJob(BackgoundJobSettings settings)
        {
            BackgroundJob.Enqueue<IDynamicJob>(j => j.Run(settings.Name));
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
