using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchandiseService.Configuration.Middlewares
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private const string grpcContentType = "application/grpc";
        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            await LogResponce(context);
        }

        public async Task LogResponce(HttpContext context)
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}