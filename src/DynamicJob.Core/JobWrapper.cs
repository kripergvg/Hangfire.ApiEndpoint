using System;
using DynamicJob.Core.JobExecutor;
using DynamicJob.Core.ServerJobStorage;

namespace DynamicJob.Core
{
    public class JobWrapper : IJobWrapper
    {
        private readonly IServerJobStorage _serverJobStorage;
        private readonly IJobExecutor _jobExecutor;
        private readonly DynamicJobConfiguration _configuration;

        public JobWrapper(IServerJobStorage serverJobStorage, IJobExecutor jobExecutor, DynamicJobConfiguration configuration)
        {
            _serverJobStorage = serverJobStorage;
            _jobExecutor = jobExecutor;
            _configuration = configuration;
        }

        public void Run(string jobName, string arguments)
        {
            if (_configuration.DistributorJobStorage != null)
            {
                var lastUpdateDate = _configuration.DistributorJobStorage.GetJobUpdateDate(jobName).Result;
                if (lastUpdateDate.HasValue)
                {
                    if (_serverJobStorage.JobsByDateExists(jobName, lastUpdateDate.Value))
                    {
                        RunJobs(jobName, lastUpdateDate.Value);
                    }
                    else
                    {
                        var job = _configuration.DistributorJobStorage.GetJobAsync(jobName).Result;
                        _serverJobStorage.Save(job.Archive, job.Name, job.UpdateDate);

                        RunJobs(jobName, job.UpdateDate);
                    }
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
