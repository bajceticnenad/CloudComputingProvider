using CloudComputingProvider.Helpers;
using Newtonsoft.Json;
using System.Text;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggerMiddleware> _logger;

    public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        await LogRequest(context);

        await LogResponseAndInvokeNext(context);
    }

    private async Task LogRequest(HttpContext context)
    {
        var correlationId = CorrelationIdHelper.GetCorrelationId(context);
        var path = $"{context?.Request?.Scheme}://{context?.Request?.Host}/{context?.Request?.Path}/{context?.Request?.Method}";
        try
        {
            var statusCode = context.Response?.StatusCode;
            var level = statusCode > 499 ? LogLevel.Error : LogLevel.Information;
            HttpRequest request = context.Request;

            using (var bodyReader = new StreamReader(request.Body))
            {
                string body = await bodyReader.ReadToEndAsync();
                request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));

                var deserializedBody = JsonConvert.DeserializeObject(body);
                var requestLog = GetLogMessage(path, correlationId, deserializedBody);

                switch (level)
                {
                    case LogLevel.Information:
                        _logger.LogInformation(requestLog);
                        break;
                    case LogLevel.Error:
                        _logger.LogError(requestLog);
                        break;
                    case LogLevel.Debug:
                        _logger.LogDebug(requestLog);
                        break;
                    case LogLevel.Warning:
                        _logger.LogWarning(requestLog);
                        break;
                    case LogLevel.Trace:
                        _logger.LogTrace(requestLog);
                        break;
                    case LogLevel.Critical:
                        _logger.LogCritical(requestLog);
                        break;
                    case LogLevel.None:
                        _logger.LogInformation(requestLog);
                        break;
                    default:
                        _logger.LogInformation(requestLog);
                        break;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private async Task LogResponseAndInvokeNext(HttpContext context)
    {
        var correlationId = CorrelationIdHelper.GetCorrelationId(context);
        var path = $"{context?.Request?.Scheme}://{context?.Request?.Host}/{context?.Request?.Path}/{context?.Request?.Method}";
        try
        {
            using (var buffer = new MemoryStream())
            {
                //replace the context response with our buffer
                var stream = context.Response.Body;
                context.Response.Body = buffer;

                //invoke the rest of the pipeline
                await _next.Invoke(context);

                var statusCode = context.Response?.StatusCode;
                var level = statusCode > 499 ? LogLevel.Error : LogLevel.Information;

                //reset the buffer and read out the contents
                buffer.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(buffer);
                using (var bufferReader = new StreamReader(buffer))
                {
                    string body = await bufferReader.ReadToEndAsync();

                    //reset to start of stream
                    buffer.Seek(0, SeekOrigin.Begin);

                    //copy our content to the original stream and put it back
                    await buffer.CopyToAsync(stream);
                    context.Response.Body = stream;

                    var deserializedBody = JsonConvert.DeserializeObject(body);
                    var responseLog = GetLogMessage(path, statusCode, correlationId, deserializedBody);

                    switch (level)
                    {
                        case LogLevel.Information:
                            _logger.LogInformation(responseLog);
                            break;
                        case LogLevel.Error:
                            _logger.LogError(responseLog);
                            break;
                        case LogLevel.Debug:
                            _logger.LogDebug(responseLog);
                            break;
                        case LogLevel.Warning:
                            _logger.LogWarning(responseLog);
                            break;
                        case LogLevel.Trace:
                            _logger.LogTrace(responseLog);
                            break;
                        case LogLevel.Critical:
                            _logger.LogCritical(responseLog);
                            break;
                        case LogLevel.None:
                            _logger.LogInformation(responseLog);
                            break;
                        default:
                            _logger.LogInformation(responseLog);
                            break;
                    }
                }
            }

        }
        catch
        {
            throw;
        }
    }

    private string GetLogMessage(string? path, int? statusCode, string? correlationId, object? deserializedBody)
    {
        return $"Path: {path}" +
               $"{Environment.NewLine}StatusCode: {statusCode}" +
               $"{Environment.NewLine}CorrelationId: {correlationId}" +
               $"{Environment.NewLine}Response: {deserializedBody}";
    }

    private string GetLogMessage(string? path, string? correlationId, object? deserializedBody)
    {
        return $"Path: {path}" +
               $"{Environment.NewLine}CorrelationId: {correlationId}" +
               $"{Environment.NewLine}Request: {deserializedBody}";
    }
}