using DynamicJob.Core;

namespace DynamicJob.SqlStorage
{
    public static class DynamicJobClientExtensions
    {
        public static DynamicJobConfiguration UserMSSQL(this DynamicJobConfiguration configuration, SqlStorageSettings settings)
        {
            configuration.DistributorJobStorage = new SqlServerJobStorage(settings);

            return configuration;
        }
    }
}
