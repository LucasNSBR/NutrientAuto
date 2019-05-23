using MediatR;
using System;

namespace NutrientAuto.Shared.Events
{
    public abstract class DomainEvent : IDomainEvent, INotification
    {
        public Guid EventId { get; }
        public DateTime EventDateCreated { get; }

        protected DomainEvent()
        {
            EventId = Guid.NewGuid();
            EventDateCreated = DateTime.Now;
        }
    }
}
