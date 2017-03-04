using System.Threading.Tasks;
using Hangfire.Api.Abstractions.JobSettings;

namespace Hangfire.Api.Client
{
    public interface IApiClient
    {
        void EnqueueBackgoundJob(BackgoundJobSettings settings);

        void AddOrUpdateRecurringJob(RecurringJobSettings settings);

        void ScheduleJob(ScheduleJobSettings settings);
    }
}
