using System.Threading.Tasks;
using DynamicJob.Core;
using Hangfire.DynamicJob.Core.JobSettings;

namespace Hangfire.DynamicJob.Core
{
    public class DynamicJobClient : IDynamicJobClient
    {
        private readonly DynamicJobConfiguration _configuration;

        public DynamicJobClient(DynamicJobConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnqueueBackgoundJobAsync(BackgoundJobSettings settings, string arguments, byte[] jobArchive)
        {
            if (_configuration.DistributorJobStorage != null)
            {
                await _configuration.DistributorJobStorage.SaveAsync(jobArchive, settings.Name).ConfigureAwait(false);
                BackgroundJob.Enqueue<IJobWrapper>(j => j.Run(settings.Name, arguments));
            }
        }

        public Task AddOrUpdateRecurringJobAsync(RecurringJobSettings settings, string arguments, byte[] jobArchive)
        {
            throw new System.NotImplementedException();
        }

        public Task ScheduleJobAsync(ScheduleJobSettings settings, string arguments, byte[] jobArchive)
        {
            throw new System.NotImplementedException();
        }
    }
}
