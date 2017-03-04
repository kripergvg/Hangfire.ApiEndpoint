using DynamicJob.Abstractions;

namespace DynamicJob.Core
{
    public class JobWrapper : IJobWrapper
    {
        private readonly IJobDependencyStorage _jobStorage;

        public JobWrapper(IJobDependencyStorage jobStorage)
        {
            _jobStorage = jobStorage;
        }

        public void Run(string jobName)
        {
            foreach (var job in _jobStorage.GetJobs(jobName))
            {
                job.Run();
            }
        }
    }
}
