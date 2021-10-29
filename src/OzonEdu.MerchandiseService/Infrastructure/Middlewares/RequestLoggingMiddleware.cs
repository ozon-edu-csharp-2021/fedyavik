using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseService.Configuration.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private const string grpcContentType = "application/grpc";
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            await _next(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            if (context.Request.ContentType == grpcContentType)
                return;
            try
            {
                _logger.LogInformation("Request logged");
                foreach (var header in context.Request.Headers)
                {
                    _logger.LogInformation($"{header.Key} {header.Value}");    
                }
                _logger.LogInformation(context.Request.Path.Value);
                
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();
                
                    var buffer = new byte[context.Request.ContentLength.Value];
                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    _logger.LogInformation(bodyAsText);

                    context.Request.Body.Position = 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request");
            }
        }
    }
}