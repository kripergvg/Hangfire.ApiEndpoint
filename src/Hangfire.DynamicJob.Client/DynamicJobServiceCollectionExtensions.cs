using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.DynamicJob.Client
{
    public static class DynamicJobServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfireClientDynamicJobs(this IServiceCollection collection)
        {
            collection.AddSingleton<IDynamicJobClient, DynamicJobClient>();

            return collection;
        }
    }
}
