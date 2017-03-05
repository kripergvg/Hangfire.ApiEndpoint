using System;
using DynamicJob.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.DynamicJob.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDynamicHangfire(this IServiceCollection collection, Action<DynamicJobConfiguration> configure = null)
        {
            collection
                .AddDynamicJobCore(configure)
                .AddSingleton<IDynamicJobClient, DynamicJobClient>();            

            return collection;
        }
    }
}
