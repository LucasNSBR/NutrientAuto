using System;

namespace NutrientAuto.Shared.IntegrationEvents.Events.Identity
{
    public class UserConfirmedEmailIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public string Email { get; }

        public UserConfirmedEmailIntegrationEvent(Guid userId, string email)
        {
            UserId = userId;
            Email = email;
        }
    }
}
