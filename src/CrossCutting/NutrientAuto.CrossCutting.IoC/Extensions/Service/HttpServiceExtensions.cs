using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.CrossCutting.HttpService.HttpContext;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Service
{
    public static partial class ServiceDependencyInjectionExtensions
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
