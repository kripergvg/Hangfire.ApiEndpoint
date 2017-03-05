using DynamicJob.Abstractions;
using DynamicJob.Core.ServerJobStorage;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicJob.Core
{
    public static class DynamicJobServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfireServerDynamicJobs(this IServiceCollection collection)
        {
            collection
                .AddSingleton<IJobWrapper, JobWrapper>()
                .AddSingleton<IServerJobStorage, ServerJobStorage.ServerJobStorage>()
                .AddSingleton<IJobExecutor, JobExecutor>();


            return collection;
        }
    }
}
