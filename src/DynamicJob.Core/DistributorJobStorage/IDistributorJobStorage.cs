using System;
using System.Threading.Tasks;

namespace DynamicJob.Core.DistributorJobStorage
{
    public interface IDistributorJobStorage
    {
        Task SaveAsync(byte[] archive, string name);

        Task<JobDependency> GetJobAsync(string name);

        Task<DateTime?> GetJobUpdateDate(string name);
    }
}
