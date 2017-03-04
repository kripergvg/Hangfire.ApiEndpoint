using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hangfire.ApiEndpoint
{
    public class ApiEndpointMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiEndpointMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                var settings = context.Request.Form["Settings"];
                var file = context.Request.Form["Job"];
                //TODO подеюажить, посмотреть что приходит
            }

        }
    }
}
