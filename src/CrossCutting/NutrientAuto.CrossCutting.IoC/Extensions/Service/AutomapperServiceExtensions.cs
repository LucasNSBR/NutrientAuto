using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.CrossCutting.Mapping.Profiles.Community;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Service
{
    public static partial class ServiceDependencyInjectionExtensions
    {
        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CommunityMappingProfile());
            });

            services.AddSingleton<IMapper, Mapper>(factory => new Mapper(mapperConfiguration));

            return services;
        }
    }
}
