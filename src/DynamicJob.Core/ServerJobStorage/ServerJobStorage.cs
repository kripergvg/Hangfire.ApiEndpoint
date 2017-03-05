using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace DynamicJob.Core.ServerJobStorage
{
    public class ServerJobStorage : IServerJobStorage
    {
        public void Save(byte[] archiveBytes, string jobName, DateTime jobUpdateDate)
        {
            using (var archiveStream = new MemoryStream(archiveBytes))
            {
                var archive = new ZipArchive(archiveStream);
                archive.ExtractToDirectory(GetJobFolder(jobName, jobUpdateDate));
            }
        }

        public IReadOnlyCollection<Type> GetJobsTypes(string jobName, DateTime jobUpdateDate)
        {
            //TODO загружать и определять джобы только при старте и обновление таски
            // сохранять их в памяти

            var jobsAssemblies = new List<Type>();
            var files = Directory.GetFiles(GetJobFolder(jobName, jobUpdateDate), "*.dll");
            foreach (var file in files)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                var jobsTypesFromAssembly = assembly
                    .GetTypes()
                    .Where(t => typeof(IJob).GetTypeInfo().IsAssignableFrom(t)
                                && t != typeof(IJob));
                jobsAssemblies.AddRange(jobsTypesFromAssembly);
            }

            return jobsAssemblies;
        }

        public IReadOnlyCollection<Type> GetJobsTypes(string jobName, string genericTypeName, DateTime jobUpdateDate)
        {
            var jobsAssemblies = new List<Type>();
            var files = Directory.GetFiles(GetJobFolder(jobName, jobUpdateDate), "*.dll");
            foreach (var file in files)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                var jobsTypesFromAssembly = assembly
                    .GetTypes()
                    .Where(t => typeof(IJob<>).GetTypeInfo().IsAssignableFrom(t));
                jobsAssemblies.AddRange(jobsTypesFromAssembly);
            }

            return jobsAssemblies;
        }

        public bool JobsByDateExists(string jobName, DateTime jobUpdateDate)
        {
            return Directory.Exists(GetJobFolder(jobName, jobUpdateDate));
        }

        private string GetJobFolder(string jobName, DateTime jobUpdateDate)
        {
            return $"{AppContext.BaseDirectory}/{Constants.JOBS_FOLDER}/{jobName}/{jobUpdateDate}";
        }
    }
}
