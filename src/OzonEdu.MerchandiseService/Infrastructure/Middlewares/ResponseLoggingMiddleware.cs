using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseService.Configuration.Middlewares
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;
        private const string grpcContentType = "application/grpc";
        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            await LogResponse(context);
        }

        public async Task LogResponse(HttpContext context)
        {
            if (context.Response.ContentType == grpcContentType)
                return;
            try
            {
                _logger.LogInformation("Response logged");
                foreach (var header in context.Response.Headers)
                {
                    _logger.LogInformation($"{header.Key} {header.Value}");    
                }
                _logger.LogInformation(context.Request.Path.Value);
                
                if (context.Response.ContentLength > 0)
                {
                    context.Request.EnableBuffering();
                
                    var buffer = new byte[context.Response.ContentLength.Value];
                    await context.Response.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    _logger.LogInformation(bodyAsText);

                    context.Request.Body.Position = 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log response");
            }
        }
    }
}