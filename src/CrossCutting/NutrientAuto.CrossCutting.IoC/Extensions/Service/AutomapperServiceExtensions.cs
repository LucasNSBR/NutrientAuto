using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Service
{
    public static partial class ServiceDependencyInjectionExtensions
    {
        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
            });

            services.AddSingleton<IMapper, Mapper>(factory => new Mapper(mapperConfiguration));

            return services;
        }
    }
}
