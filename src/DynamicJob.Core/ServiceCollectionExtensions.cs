using System;
using DynamicJob.Core.JobExecutor;
using DynamicJob.Core.ServerJobStorage;
using Microsoft.Extensions.DependencyInjection;

namespace DynamicJob.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDynamicJobCore(this IServiceCollection collection, Action<DynamicJobConfiguration> configure = null)
        {
            collection
                .AddSingleton<IJobWrapper, JobWrapper>()
                .AddSingleton<IServerJobStorage, ServerJobStorage.ServerJobStorage>()
                .AddSingleton<IJobExecutor, JobExecutor.JobExecutor>();

            //TODO сделать дефолтное хранилище в памяти
            var configuration = new DynamicJobConfiguration();
            configure?.Invoke(configuration);
            collection.AddSingleton(configuration);

            return collection;
        }
    }
}
