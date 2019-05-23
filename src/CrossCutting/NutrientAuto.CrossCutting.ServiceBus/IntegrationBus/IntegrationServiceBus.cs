using MassTransit;
using NutrientAuto.Shared.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.ServiceBus.IntegrationBus
{
    public class IntegrationServiceBus : IIntegrationServiceBus
    {
        private readonly IBusControl _bus;

        public IntegrationServiceBus(IBusControl bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent) where TIntegrationEvent : IntegrationEvent
        {
            await _bus.Publish(integrationEvent);
        }
    }
}
