using Microsoft.AspNetCore.Builder;

namespace Hangfire.ApiEndpoint
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiEndpoint(this IApplicationBuilder appBuilder, string path = "/api")
        {
            appBuilder.Map(path, a => a.UseMiddleware<ApiEndpointMiddleware>());
            return appBuilder;
        }
    }
}
