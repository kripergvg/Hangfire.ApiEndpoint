using System;
using DynamicJob.Abstractions;
using DynamicJob.Core.DistributorJobStorage;
using DynamicJob.Core.JobExecutor;
using DynamicJob.Core.ServerJobStorage;

namespace DynamicJob.Core
{
    public class JobWrapper : IJobWrapper
    {
        private readonly IServerJobStorage _serverJobStorage;
        private readonly IDistributorJobStorage _distributorJobStorage;
        private readonly IJobExecutor _jobExecutor;

        public JobWrapper(IServerJobStorage serverJobStorage, IDistributorJobStorage distributorJobStorage, IJobExecutor jobExecutor)
        {
            _serverJobStorage = serverJobStorage;
            _distributorJobStorage = distributorJobStorage;
            _jobExecutor = jobExecutor;
        }

        public void Run(string jobName, string arguments)
        {
            var lastUpdateDate = _distributorJobStorage.GetJobUpdateDate(jobName).Result;
            if (lastUpdateDate.HasValue)
            {
                if (_serverJobStorage.JobsByDateExists(jobName, lastUpdateDate.Value))
                {
                    RunJobs(jobName, lastUpdateDate.Value);
                }
                else
                {
                    var job = _distributorJobStorage.GetJobAsync(jobName).Result;
                    _serverJobStorage.Save(job.Archive, job.Name, job.UpdateDate);

                    RunJobs(jobName, job.UpdateDate);
                }
            }
        }

        private void RunJobs(string jobName, DateTime jobDate)
        {
            //TODO разделить на джобы с параметрами и без
            // проверять, что тип Gneric подходит под текущий тип аргумента
            foreach (var jobType in _serverJobStorage.GetJobsTypes(jobName, jobDate))
            {
                _jobExecutor.Execute(jobType);
            }
        }
    }
}
