using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.CrossCutting.EmailService.Configuration;
using NutrientAuto.CrossCutting.EmailService.Services.Dispatcher;
using System;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Service
{
    public static partial class ServiceDependencyInjectionExtensions
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, Action<EmailServiceOptions> setupAction)
        {
            services.Configure(setupAction);
            services.AddTransient<IEmailDispatcher, EmailDispatcher>();

            return services;
        }
    }
}
