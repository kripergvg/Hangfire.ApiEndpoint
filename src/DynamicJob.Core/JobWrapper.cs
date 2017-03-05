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
            //TODO разделить на джобы с параметрами и без
            // проверять, что тип Gneric подходит под текущий тип аргумента
            foreach (var jobType in _jobStorage.GetJobsTypes(jobName))
            {
                _jobExecutor.Execute(jobType);
            }
        }
    }
}
