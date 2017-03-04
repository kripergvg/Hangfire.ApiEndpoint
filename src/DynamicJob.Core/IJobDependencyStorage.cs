using System.Collections.Generic;

namespace DynamicJob.Core
{
    public interface IJobDependencyStorage
    {
        void Save(byte[] archiveBytes, string jobName);

        IReadOnlyCollection<IJob> GetJobs(string jobName);
    }
}
