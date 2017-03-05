using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace DynamicJob.Core
{
    public class JobDependencyStorage : IJobDependencyStorage
    {
        public void Save(byte[] archiveBytes, string jobName)
        {
            using (var archiveStream = new MemoryStream(archiveBytes))
            {
                var archive = new ZipArchive(archiveStream);
                archive.ExtractToDirectory($"{AppContext.BaseDirectory}/{Constants.JOBS_FOLDER}/{jobName}");
            }
        }

        public IReadOnlyCollection<Type> GetJobsTypes(string jobName)
        {
            //TODO загружать и определять джобы только при старте и обновление таски
            // сохранять их в памяти

            var jobsAssemblies = new List<Type>();
            var files = Directory.GetFiles($"{AppContext.BaseDirectory}/{Constants.JOBS_FOLDER}/{jobName}", "*.dll");
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
    }
}
