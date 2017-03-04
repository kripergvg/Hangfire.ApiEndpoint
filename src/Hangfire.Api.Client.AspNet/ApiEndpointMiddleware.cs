using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Api.Abstractions;
using Hangfire.Api.Abstractions.JobSettings;
using Hangfire.Api.Client;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hangfire.ApiEndpoint
{
    public class ApiEndpointMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApiClient _apiClient;

        public ApiEndpointMiddleware(RequestDelegate next, IApiClient apiClient)
        {
            _next = next;
            _apiClient = apiClient;
        }

        public async Task Invoke(HttpContext context)
        {

            var type = (JobType)Int32.Parse(context.Request.Form[RegisterJobConstants.TYPE].First());
            var settingsString = context.Request.Form[RegisterJobConstants.SETTINGS].First();

            switch (type)
            {
                case JobType.Background:
                     _apiClient.EnqueueBackgoundJob(JsonConvert.DeserializeObject<BackgoundJobSettings>(settingsString));
                    break;
                case JobType.Recurring:
                     _apiClient.AddOrUpdateRecurringJob(JsonConvert.DeserializeObject<RecurringJobSettings>(settingsString));
                    break;
                case JobType.Schedule:
                     _apiClient.ScheduleJob(JsonConvert.DeserializeObject<ScheduleJobSettings>(settingsString));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}
