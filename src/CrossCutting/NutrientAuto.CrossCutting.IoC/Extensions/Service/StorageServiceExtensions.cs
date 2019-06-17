using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.CrossCutting.IoC.Configuration.Service;
using NutrientAuto.CrossCutting.Storage.Configuration;
using NutrientAuto.CrossCutting.Storage.Services.StorageService;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Service
{
    public static partial class ServiceDependencyInjectionExtensions
    {
        public static IServiceCollection AddStorageService(this IServiceCollection services, StorageOptions storageOptions)
        {
            services.Configure<AccountOptions>(opt =>
            {
                opt.AccountKey = storageOptions.AccountKey;
                opt.AccountName = storageOptions.AccountName;
            });

            services.Configure<ContainerOptions>(opt =>
            {
                opt.ProfileImageContainerName = storageOptions.ProfileImageContainerName;
                opt.PostImageContainerName = storageOptions.PostImageContainerName;
                opt.MeasureImageContainerName = storageOptions.MeasureImageContainerName;
            });

            services.AddTransient<IStorageService, StorageService>();

            return services;
        }
    }
}
