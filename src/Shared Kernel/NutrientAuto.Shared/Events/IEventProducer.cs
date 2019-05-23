using System.Collections.Generic;

namespace NutrientAuto.Shared.Events
{
    public interface IEventProducer
    {
        IReadOnlyList<DomainEvent> GetEvents();
        void AddEvent(DomainEvent @event);
        bool HasEvents();
    }
}
