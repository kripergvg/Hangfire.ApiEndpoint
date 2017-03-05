namespace DynamicJob.SqlStorage
{
    public class SqlStorageSettings
    {
        public SqlStorageSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
