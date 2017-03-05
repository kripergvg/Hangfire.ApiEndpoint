using System.Threading.Tasks;
using Hangfire.DynamicJob.Client.JobSettings;

namespace Hangfire.DynamicJob.Client
{
    public interface IDynamicJobClient
    {
        Task EnqueueBackgoundJobAsync(BackgoundJobSettings settings, string arguments, byte[] jobArchive);

        Task AddOrUpdateRecurringJobAsync(RecurringJobSettings settings, string arguments, byte[] jobArchive);

        Task ScheduleJobAsync(ScheduleJobSettings settings, string arguments, byte[] jobArchive);
    }
}
