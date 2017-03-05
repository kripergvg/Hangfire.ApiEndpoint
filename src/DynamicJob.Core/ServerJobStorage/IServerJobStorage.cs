using System;
using System.Collections.Generic;

namespace DynamicJob.Core.ServerJobStorage
{
    public interface IServerJobStorage
    {
        void Save(byte[] archiveBytes, string jobName, DateTime jobUpdateDate);

        IReadOnlyCollection<Type> GetJobsTypes(string jobName, DateTime jobUpdateDate);

        IReadOnlyCollection<Type> GetJobsTypes(string jobName, string genericTypeName, DateTime jobUpdateDate);

        bool JobsByDateExists(string jobName, DateTime jobUpdateDate);
    }
}
