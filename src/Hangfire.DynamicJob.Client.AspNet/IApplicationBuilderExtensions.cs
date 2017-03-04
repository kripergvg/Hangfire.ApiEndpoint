using Microsoft.AspNetCore.Builder;

namespace Hangfire.DynamicJob.Client.AspNet
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHangfireClientDynamicJobs(this IApplicationBuilder appBuilder, string path = "/api")
        {
            appBuilder.Map(path, a => a.UseMiddleware<ApiEndpointMiddleware>());
            return appBuilder;
        }
    }
}
