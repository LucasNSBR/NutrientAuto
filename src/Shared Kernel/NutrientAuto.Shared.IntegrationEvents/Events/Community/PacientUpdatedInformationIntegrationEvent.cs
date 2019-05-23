using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.IntegrationEvents.Events.Community
{
    public class PacientUpdatedInformationIntegrationEvent : IntegrationEvent
    {
        public Guid PacientId { get; }
        public string Name { get; }
        public EmailAddress EmailAddress { get; }
        public DateTime BirthDate { get; }
        public string Bio { get; }
        public Address Address { get; }
        public bool IsPublic { get; }
        public PhoneNumber PhoneNumber { get; }
        public PhoneNumber MobileNumber { get; }

        public PacientUpdatedInformationIntegrationEvent(Guid pacientId, string name, EmailAddress emailAddress, DateTime birthDate, string bio, Address address, bool isPublic, PhoneNumber phoneNumber, PhoneNumber mobileNumber)
        {
            PacientId = pacientId;
            Name = name;
            Address = address;
            IsPublic = isPublic;
            BirthDate = birthDate;
            Bio = bio;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
        }
    }
}
