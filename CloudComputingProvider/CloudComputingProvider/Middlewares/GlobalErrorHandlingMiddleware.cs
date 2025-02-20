using CloudComputingProvider.BusinessModel;
using CloudComputingProvider.Extensions.Exceptions;
using FluentValidation;
using Newtonsoft.Json;
using System.Net;
using KeyNotFoundException = CloudComputingProvider.Extensions.Exceptions.KeyNotFoundException;
using NotImplementedException = CloudComputingProvider.Extensions.Exceptions.NotImplementedException;
using UnauthorizedAccessException = CloudComputingProvider.Extensions.Exceptions.UnauthorizedAccessException;

namespace CloudComputingProvider.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Path: {context.Request.Path}");

                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            try
            {
                HttpStatusCode status;
                string message;
                var exceptionType = exception.GetType();

                if (exceptionType == typeof(BadRequestException))
                {
                    message = exception.Message;
                    status = HttpStatusCode.BadRequest;
                }
                else if (exceptionType == typeof(NotFoundException))
                {
                    message = exception.Message;
                    status = HttpStatusCode.NotFound;
                }
                else if (exceptionType == typeof(NotImplementedException))
                {
                    status = HttpStatusCode.NotImplemented;
                    message = exception.Message;
                }
                else if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    status = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                }
                else if (exceptionType == typeof(KeyNotFoundException))
                {
                    status = HttpStatusCode.Unauthorized;
                    message = exception.Message;
                }
                else if (exceptionType == typeof(ValidationException))
                {
                    status = HttpStatusCode.BadRequest;
                    message = exception.Message;
                }
                else
                {
                    status = HttpStatusCode.InternalServerError;
                    message = exception.Message;
                }

                var exceptionResult = JsonConvert.SerializeObject(new Response()
                {
                    Success = false,
                    ResponseMessage = message
                });

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)status;
                await context.Response.WriteAsync(exceptionResult);

                var deserializedBody = JsonConvert.DeserializeObject(exceptionResult);
                _logger.LogInformation($"Response: {deserializedBody}", context.Request.Method, context.Request.Path, context.Response.StatusCode);

                return;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Path: {context.Request.Path}");
                throw;
            }
        }
    }
}
