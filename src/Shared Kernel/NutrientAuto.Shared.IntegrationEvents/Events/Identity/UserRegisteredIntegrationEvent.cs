using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.IntegrationEvents.Events.Identity
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; }
        public Genre Genre { get; }
        public string Name { get; }
        public string Username { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        public UserRegisteredIntegrationEvent(Guid userId, Genre genre, string name, string username, string email, DateTime birthDate)
        {
            UserId = userId;
            Genre = genre;
            Name = name;
            Username = username;
            Email = email;
            BirthDate = birthDate;
        }
    }
}
