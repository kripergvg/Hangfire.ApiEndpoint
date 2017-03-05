using System;

namespace DynamicJob.Core
{
    public interface IJobExecutor
    {
        void Execute(Type jobType, string arguments);
    }
}
