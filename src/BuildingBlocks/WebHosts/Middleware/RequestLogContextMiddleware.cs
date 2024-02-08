using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace WebHosts.Middleware
{
    public class RequestLogContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogContextMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        public Task InvokeAsync(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
            {
                return _next(context);
            }

        }
    }
}
