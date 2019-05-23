using GreenPipes;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.CrossCutting.IoC.Configuration.Service;
using NutrientAuto.CrossCutting.ServiceBus.BackgroundTasks;
using NutrientAuto.CrossCutting.ServiceBus.IntegrationBus;
using System;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Service
{
    public static partial class ServiceDependencyInjectionExtensions
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services, Action<ServiceBusOptions> setupAction)
        {
            ServiceBusOptions options = new ServiceBusOptions();
            setupAction.Invoke(options);

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<Community.Domain.IntegrationEventHandlers.Identity.IdentityIntegrationEventHandler>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(transport =>
            {
                Uri hostAddress = new Uri(options.HostAddress);

                IRabbitMqHost host = transport.Host(hostAddress, cfg =>
                {
                    cfg.Username(options.RabbitMqHostUser);
                    cfg.Password(options.RabbitMqHostPassword);
                });

                transport.ReceiveEndpoint(host, options.RabbitMqQueueName, endpoint =>
                {
                    endpoint.UseRetry(r => r.Immediate(3));

                    endpoint.Consumer<Community.Domain.IntegrationEventHandlers.Identity.IdentityIntegrationEventHandler>(provider);
                });
            }));

            services.AddSingleton<IIntegrationServiceBus, IntegrationServiceBus>();
            services.AddHostedService<MassTransitProcess>();

            return services;
        }
    }
}
