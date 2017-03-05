using System.Threading.Tasks;
using DynamicJob.Abstractions;
using DynamicJob.Core;
using DynamicJob.Core.DistributorJobStorage;
using Hangfire.DynamicJob.Client.JobSettings;

namespace Hangfire.DynamicJob.Client
{
    public class DynamicJobClient : IDynamicJobClient
    {
        private readonly IDistributorJobStorage _jobStorage;

        public DynamicJobClient(IDistributorJobStorage jobStorage)
        {
            _jobStorage = jobStorage;
        }

        public async Task EnqueueBackgoundJobAsync(BackgoundJobSettings settings, string arguments, byte[] jobArchive)
        {
            await _jobStorage.SaveAsync(jobArchive, settings.Name).ConfigureAwait(false);
            BackgroundJob.Enqueue<IJobWrapper>(j => j.Run(settings.Name, arguments));
        }

        public Task AddOrUpdateRecurringJobAsync(RecurringJobSettings settings, string arguments)
        {
            throw new System.NotImplementedException();
        }

        public Task ScheduleJobAsync(ScheduleJobSettings settings, string arguments)
        {
            throw new System.NotImplementedException();
        }
    }
}
