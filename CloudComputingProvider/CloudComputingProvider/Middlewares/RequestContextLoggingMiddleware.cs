using CloudComputingProvider.Helpers;
using Serilog.Context;

namespace CloudComputingProvider.Middlewares
{
    public class RequestContextLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestContextLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            string correlationId = CorrelationIdHelper.GetCorrelationId(context);

            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                return _next.Invoke(context);
            }
        }
    }
}
