using Microsoft.Extensions.Primitives;

namespace CloudComputingProvider.Helpers
{
    public static class CorrelationIdHelper
    {
        #region PrivateFields
        private const string CorrelationIdHeaderName = "X-Correlation-Id";
        #endregion PrivateFields

        #region PublicMethods

        public static string GetCorrelationId(HttpContext context)
        {
            context.Request.Headers.TryGetValue(
                CorrelationIdHeaderName, out StringValues correlationId);

            return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
        }

        #endregion PublicMethods
    }
}
