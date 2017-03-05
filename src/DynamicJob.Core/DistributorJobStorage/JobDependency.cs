using System;

namespace DynamicJob.Core.DistributorJobStorage
{
    public class JobDependency
    {
        public JobDependency(string name, byte[] archive, DateTime createDate, DateTime updateDate)
        {
            Name = name;
            Archive = archive;
            CreateDate = createDate;
            UpdateDate = updateDate;
        }

        public string Name { get; set; }

        public byte[] Archive { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
