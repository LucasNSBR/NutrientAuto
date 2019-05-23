using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Middlewares
{
    public class NotFoundLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILoggerFactory loggerFactory)
        {
            ILogger logger = loggerFactory.CreateLogger("NotFoundFilter");

            await _next(httpContext);

            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                logger.LogWarning("404 - O usuário buscou uma rota inexistente: {route}", httpContext.Request.GetDisplayUrl());
        }
    }

    public static class NotFoundLoggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseNotFoundLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<NotFoundLoggerMiddleware>();
        }
    }
}
