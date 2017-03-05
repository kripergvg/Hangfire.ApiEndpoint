using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DynamicJob.Core;
using Hangfire.DynamicJob.Core;
using Hangfire.DynamicJob.Core.JobSettings;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hangfire.DynamicJob.Client.AspNet
{
    public class ApiEndpointMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDynamicJobClient _apiClient;

        public ApiEndpointMiddleware(RequestDelegate next, IDynamicJobClient apiClient)
        {
            _next = next;
            _apiClient = apiClient;
        }

        public async Task Invoke(HttpContext context)
        {
            var type = (JobType)Int32.Parse(context.Request.Form[RegisterJobConstants.TYPE].First());
            var settingsString = context.Request.Form[RegisterJobConstants.SETTINGS].First();

            var arguments = context.Request.Form[RegisterJobConstants.ARGUMENTS].FirstOrDefault();

            var jobrAchive = context.Request.Form.Files[RegisterJobConstants.JOB_ARCHIVE_NAME];
            var jobBytes = await FileToByte(jobrAchive).ConfigureAwait(false);

            switch (type)
            {
                case JobType.Background:
                    await _apiClient.EnqueueBackgoundJobAsync(JsonConvert.DeserializeObject<BackgoundJobSettings>(settingsString), arguments, jobBytes);
                    break;
                case JobType.Recurring:
                    await _apiClient.AddOrUpdateRecurringJobAsync(JsonConvert.DeserializeObject<RecurringJobSettings>(settingsString), arguments, jobBytes);
                    break;
                case JobType.Schedule:
                    await _apiClient.ScheduleJobAsync(JsonConvert.DeserializeObject<ScheduleJobSettings>(settingsString), arguments, jobBytes);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private async Task<byte[]> FileToByte(IFormFile file)
        {
            using (var archiveStream = new MemoryStream())
            {
                using (file.OpenReadStream())
                {
                    await file.CopyToAsync(archiveStream).ConfigureAwait(false);
                }

                return archiveStream.ToArray();
            }
        }
    }
}
