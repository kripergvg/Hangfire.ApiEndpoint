using Microsoft.AspNetCore.Builder;

namespace Hangfire.Api.Server.AspNet
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseServerApi(this IApplicationBuilder appBuilder, string path = "/api")
        {
            appBuilder.Map(path, a => a.UseMiddleware<AspNetServerApiMiddleware>());
            return appBuilder;
        }
    }
}
