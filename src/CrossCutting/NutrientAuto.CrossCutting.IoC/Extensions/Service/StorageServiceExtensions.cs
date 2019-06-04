using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.CrossCutting.Storage.Configuration;
using NutrientAuto.CrossCutting.Storage.Services.StorageDefinitions;
using NutrientAuto.CrossCutting.Storage.Services.StorageService;
using System;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Service
{
    public static partial class ServiceDependencyInjectionExtensions
    {
        public static IServiceCollection AddStorageService(this IServiceCollection services, Action<StorageOptions> setupAction)
        {
            services.Configure(setupAction);
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<IStorageDefinitions, StorageDefinitions>();

            return services;
        }
    }
}
