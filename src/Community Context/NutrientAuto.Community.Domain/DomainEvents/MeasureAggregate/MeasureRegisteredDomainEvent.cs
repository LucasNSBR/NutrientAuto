using NutrientAuto.Shared.Events;
using System;

namespace NutrientAuto.Community.Domain.DomainEvents.MeasureAggregate
{
    public class MeasureRegisteredDomainEvent : DomainEvent
    {
        public Guid MeasureId { get; }
        public Guid ProfileId { get; }
        public bool WritePost { get; }

        public MeasureRegisteredDomainEvent(Guid measureId, Guid profileId, bool writePost)
        {
            MeasureId = measureId;
            ProfileId = profileId;
            WritePost = writePost;
        }
    }
}
