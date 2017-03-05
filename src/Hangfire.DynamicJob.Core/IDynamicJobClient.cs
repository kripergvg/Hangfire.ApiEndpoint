using System.Threading.Tasks;
using Hangfire.DynamicJob.Core.JobSettings;

namespace Hangfire.DynamicJob.Core
{
    public interface IDynamicJobClient
    {
        Task EnqueueBackgoundJobAsync(BackgoundJobSettings settings, string arguments, byte[] jobArchive);

        Task AddOrUpdateRecurringJobAsync(RecurringJobSettings settings, string arguments, byte[] jobArchive);

        Task ScheduleJobAsync(ScheduleJobSettings settings, string arguments, byte[] jobArchive);
    }
}
