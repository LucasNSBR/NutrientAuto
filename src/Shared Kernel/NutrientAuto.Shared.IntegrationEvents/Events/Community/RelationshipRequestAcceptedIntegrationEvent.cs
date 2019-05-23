using System;

namespace NutrientAuto.Shared.IntegrationEvents.Events.Community
{
    public class RelationshipRequestAcceptedIntegrationEvent : IntegrationEvent
    {
        public Guid RelationshipId { get; }
        public Guid NutritionistId { get; }
        public Guid PacientId { get; }
        
        public RelationshipRequestAcceptedIntegrationEvent(Guid relationshipId, Guid nutritionistId, Guid pacientId)
        {
            RelationshipId = relationshipId;
            NutritionistId = nutritionistId;
            PacientId = pacientId;
        }
    }
}
