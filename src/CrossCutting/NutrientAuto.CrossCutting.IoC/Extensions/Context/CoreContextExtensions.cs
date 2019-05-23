using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.Shared.Notifications;
using NutrientAuto.Shared.PipelineBehaviours;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Context
{
    public static partial class ContextDependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreContext(this IServiceCollection services)
        {
            services.AddMediatR();

            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandValidatorPipelineBehaviour<,>));

            return services;
        }
    }
}
