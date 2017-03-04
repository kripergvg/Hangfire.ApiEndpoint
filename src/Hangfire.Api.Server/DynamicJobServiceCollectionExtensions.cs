using DynamicJob;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.Api.Server
{
    public static class DynamicJobServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfireServerDynamicJobs(this IServiceCollection collection)
        {
            collection.AddSingleton<IDynamicJob, Job>();

            return collection;
        }
    }
}
