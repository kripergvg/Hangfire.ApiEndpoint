using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DynamicJob.Core;
using DynamicJob.Core.DistributorJobStorage;

namespace DynamicJob.SqlStorage
{
    public class SqlServerJobStorage : IDistributorJobStorage
    {
        private readonly SqlStorageSettings _settings;

        public SqlServerJobStorage(SqlStorageSettings settings)
        {
            _settings = settings;
        }

        public async Task SaveAsync(byte[] archive, string name)
        {
            await CreateTableIfNotExistAsync();

            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                var updateOrCreateCommand = new SqlCommand(@"IF EXISTS(SELECT 1 FROM DynamicJob.JobDependencyStorage)
	                                                        UPDATE DynamicJob.JobDependencyStorage
	                                                        SET UpdateDate=@UpdateDate,
		                                                        Dependency=@Dependency
	                                                        WHERE Name=@Name
                                                        ELSE
	                                                        INSERT INTO DynamicJob.JobDependencyStorage(Name, Dependency, CreateDate, UpdateDate)
	                                                        VALUES(@Name, @Dependency, @CreateDate, @UpdateDate)
                                                            ", connection);
                updateOrCreateCommand.Parameters.AddWithValue("@Name", name);
                updateOrCreateCommand.Parameters.AddWithValue("@Dependency", archive);
                updateOrCreateCommand.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                updateOrCreateCommand.Parameters.AddWithValue("@UpdateDate", DateTime.Now);

                await connection.OpenAsync().ConfigureAwait(false);
                await updateOrCreateCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async Task<JobDependency> GetJobAsync(string name)
        {
            await CreateTableIfNotExistAsync();

            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                var getJobCommand = new SqlCommand(@"SELECT Name, Dependency, CreateDate, UpdateDate
                                                    FROM DynamicJob.JobDependencyStorage
                                                    WHERE Name=@Name", connection);
                getJobCommand.Parameters.AddWithValue("@Name", name);

                await connection.OpenAsync().ConfigureAwait(false);
                using (var reader = await getJobCommand.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    var readed = await reader.ReadAsync();
                    if (readed)
                    {
                        return new JobDependency((string)reader["Name"], (byte[])reader["Dependency"], (DateTime)reader["CreateDate"], (DateTime)reader["UpdateDate"]);
                    }
                    else
                    {
                        return null;
                    }
                }

            }
        }

        public async Task<DateTime?> GetJobUpdateDate(string name)
        {
            await CreateTableIfNotExistAsync();

            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                var getJobCommand = new SqlCommand(@"SELECT UpdateDate
                                                    FROM DynamicJob.JobDependencyStorage
                                                    WHERE Name=@Name", connection);
                getJobCommand.Parameters.AddWithValue("@Name", name);

                await connection.OpenAsync().ConfigureAwait(false);
                var updateDate = (DateTime?)await getJobCommand.ExecuteScalarAsync().ConfigureAwait(false);
                return updateDate;
            }
        }

        // перенести в регистрацию
        private async Task CreateTableIfNotExistAsync()
        {
            using (var connection = new SqlConnection(_settings.ConnectionString))
            {
                var createTableCommand = new SqlCommand(@"BEGIN TRAN
                                                        IF NOT EXISTS(SELECT name FROM sys.schemas WHERE name = 'DynamicJob')
                                                            EXEC('CREATE SCHEMA DynamicJob')
                                                        IF OBJECT_ID(N'DynamicJob.JobDependencyStorage', N'U') IS NULL
                                                            CREATE TABLE DynamicJob.JobDependencyStorage
                                                            (
                                                                Name nvarchar(255),
                                                                Dependency varbinary(max),
                                                                CreateDate datetime,
                                                                UpdateDate datetime
                                                            )
                                                        COMMIT", connection);
                await connection.OpenAsync();
                await createTableCommand.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}
