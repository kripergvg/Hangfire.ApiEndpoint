using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hangfire.Api.Server.AspNet
{
    public class AspNetServerApiMiddleware
    {
        private readonly RequestDelegate _next;

        public AspNetServerApiMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var t = new ServerApi();
            t.AcceptJob();
        }
    }
}
