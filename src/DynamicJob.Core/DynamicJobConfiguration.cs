using System;
using DynamicJob.Core.DistributorJobStorage;

namespace DynamicJob.Core
{
    public class DynamicJobConfiguration
    {
        private string _jobsStoragePath;

        public IDistributorJobStorage DistributorJobStorage { get; set; }

        public string JobsStoragePath
        {
            get
            {
                if (String.IsNullOrEmpty(_jobsStoragePath))
                {
                    return _jobsStoragePath = $"{AppContext.BaseDirectory}/{Constants.DEFAULT_JOBS_FOLDER}";
                }

                return _jobsStoragePath;
            }

            set { _jobsStoragePath = value; }
        }
    }
}
