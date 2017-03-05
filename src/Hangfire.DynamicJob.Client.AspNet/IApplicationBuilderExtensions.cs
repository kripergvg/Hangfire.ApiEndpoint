using Microsoft.AspNetCore.Builder;

namespace Hangfire.DynamicJob.Client.AspNet
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHangfireDynamicJobs(this IApplicationBuilder appBuilder, string path = "/api")
        {
            appBuilder.Map(path, a => a.UseMiddleware<ApiEndpointMiddleware>());
            return appBuilder;
        }
    }
}
