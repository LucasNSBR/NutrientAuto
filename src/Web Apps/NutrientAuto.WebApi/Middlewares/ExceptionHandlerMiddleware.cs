using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILoggerFactory loggerFactory)
        {
            ILogger logger = loggerFactory.CreateLogger("GlobalExceptionFilter");

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "Global Exception - {message}", ex.Message);

                httpContext.Response.Clear();
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                string response = JsonConvert.SerializeObject(new {
                    success = false,
                    message = "Ocorreu um erro inesperado no bloco principal da aplicação, tente novamente mais tarde."
                });

                await httpContext.Response.WriteAsync(response);
            }
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
