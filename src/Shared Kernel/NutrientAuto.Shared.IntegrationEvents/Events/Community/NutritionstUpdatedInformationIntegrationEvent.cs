using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.IntegrationEvents.Events.Community
{
    public class NutritionstUpdatedInformationIntegrationEvent : IntegrationEvent
    {
        public Guid NutritionistId { get; }
        public string Name { get; }
        public EmailAddress EmailAddress { get; }
        public DateTime BirthDate { get; }
        public string Bio { get; }
        public CrnNumber CrnNumber { get; }
        public Address Address { get; }
        public PhoneNumber PhoneNumber { get; }
        public PhoneNumber MobileNumber { get; }

        public NutritionstUpdatedInformationIntegrationEvent(Guid nutritionistId, string name, EmailAddress emailAddress, DateTime birthDate, string bio, Address address, PhoneNumber phoneNumber, PhoneNumber mobileNumber, CrnNumber crnNumber)
        {
            NutritionistId = nutritionistId;
            Name = name;
            Address = address;
            BirthDate = birthDate;
            Bio = bio;
            CrnNumber = crnNumber;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
        }
    }
}
