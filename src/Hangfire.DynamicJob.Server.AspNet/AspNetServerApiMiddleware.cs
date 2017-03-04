using System.IO;
using System.Threading.Tasks;
using DynamicJob.Core;
using Microsoft.AspNetCore.Http;

namespace Hangfire.DynamicJob.Server.AspNet
{
    public class AspNetServerApiMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJobDependencyStorage _jobStorage;

        public AspNetServerApiMiddleware(RequestDelegate next, IJobDependencyStorage jobStorage)
        {
            _next = next;
            _jobStorage = jobStorage;
        }

        public async Task Invoke(HttpContext context)
        {
            var jobName = context.Request.Form[Constants.JOB_NAME];
            var jobrchive = context.Request.Form.Files[Constants.JOB_ARCHIVE_NAME];

            using (var archiveStream = new MemoryStream())
            {
                using (jobrchive.OpenReadStream())
                {
                    await jobrchive.CopyToAsync(archiveStream);
                }

                var jobArchiveBytes = archiveStream.ToArray();

                _jobStorage.Save(jobArchiveBytes, jobName);
            }
        }
    }
}
