using DynamicJob.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicJob.Core
{
    public static class DynamicJobServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfireServerDynamicJobs(this IServiceCollection collection)
        {
            collection
                .AddSingleton<IJobWrapper, JobWrapper>()
                .AddSingleton<IJobDependencyStorage, JobDependencyStorage>();


            return collection;
        }
    }
}
