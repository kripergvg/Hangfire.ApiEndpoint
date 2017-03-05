using DynamicJob.Abstractions;

namespace DynamicJob.Core
{
    public class JobWrapper : IJobWrapper
    {
        private readonly IJobDependencyStorage _jobStorage;
        private readonly IJobExecutor _jobExecutor;

        public JobWrapper(IJobDependencyStorage jobStorage, IJobExecutor jobExecutor)
        {
            _jobStorage = jobStorage;
            _jobExecutor = jobExecutor;
        }

        public void Run(string jobName, string arguments)
        {
            foreach (var jobType in _jobStorage.GetJobsTypes(jobName))
            {
                _jobExecutor.Execute(jobType, arguments);
            }
        }
    }
}
