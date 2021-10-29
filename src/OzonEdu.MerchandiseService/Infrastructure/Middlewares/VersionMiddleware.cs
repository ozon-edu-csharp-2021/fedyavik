using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseService.Configuration.Middlewares
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var _version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version";
            var response = new
            {
                version = _version,
                serviceName = "MerchandiseService"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}