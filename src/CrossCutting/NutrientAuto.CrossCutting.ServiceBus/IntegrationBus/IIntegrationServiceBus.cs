using NutrientAuto.Shared.IntegrationEvents.Events;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.ServiceBus.IntegrationBus
{
    public interface IIntegrationServiceBus
    {
        Task PublishAsync<T>(T integrationEvent) where T : IntegrationEvent;
    }
}
