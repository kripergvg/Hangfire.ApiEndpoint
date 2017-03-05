using System;

namespace DynamicJob.Core
{
    public interface IJobExecutor
    {
        void Execute(Type jobType, string arguments);

        void Execute(Type jobType);
    }
}
