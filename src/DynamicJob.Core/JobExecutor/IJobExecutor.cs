using System;

namespace DynamicJob.Core.JobExecutor
{
    public interface IJobExecutor
    {
        void Execute(Type jobType, string arguments);

        void Execute(Type jobType);
    }
}
