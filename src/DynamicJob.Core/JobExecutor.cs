using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DynamicJob.Core
{
    public class JobExecutor : IJobExecutor
    {
        public void Execute(Type jobType, string arguments)
        {
            if (typeof(IJob<>).GetTypeInfo().IsAssignableFrom(jobType))
            {
                ExecuteJob(jobType, arguments);
            }
            else if (typeof(IJob).GetTypeInfo().IsAssignableFrom(jobType))
            {
                ExecuteJob(jobType);
            }
        }

        private void ExecuteJob(Type jobType, string arguments)
        {
            var argumentType = jobType.GenericTypeArguments.First();
            var argument = JsonConvert.DeserializeObject(arguments, argumentType);

            var job = Activator.CreateInstance(jobType);
            var jobMethod = jobType.GetRuntimeMethod(nameof(IJob<string>.Run), new[] { argumentType });
            jobMethod.Invoke(job, new[] { argument });
        }

        private void ExecuteJob(Type jobType)
        {
            var job = (IJob)Activator.CreateInstance(jobType);
            job.Run();
        }
    }
}
