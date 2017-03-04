using Microsoft.AspNetCore.Builder;

namespace Hangfire.DynamicJob.Server.AspNet
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHangfireServerDynamicJobs(this IApplicationBuilder appBuilder, string path = "/dynamicJobSave")
        {
            appBuilder.Map(path, a => a.UseMiddleware<AspNetServerApiMiddleware>());
            return appBuilder;
        }
    }
}
