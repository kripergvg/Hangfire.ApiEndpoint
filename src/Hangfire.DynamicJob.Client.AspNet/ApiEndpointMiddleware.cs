using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.DynamicJob.Client.JobSettings;
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

            switch (type)
            {
                case JobType.Background:
                    _apiClient.EnqueueBackgoundJob(JsonConvert.DeserializeObject<BackgoundJobSettings>(settingsString), arguments);
                    break;
                case JobType.Recurring:
                    _apiClient.AddOrUpdateRecurringJob(JsonConvert.DeserializeObject<RecurringJobSettings>(settingsString), arguments);
                    break;
                case JobType.Schedule:
                    _apiClient.ScheduleJob(JsonConvert.DeserializeObject<ScheduleJobSettings>(settingsString), arguments);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
