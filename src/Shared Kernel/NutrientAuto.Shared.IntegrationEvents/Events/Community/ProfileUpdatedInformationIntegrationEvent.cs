using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.IntegrationEvents.Events.Community
{
    public class ProfileUpdatedInformationIntegrationEvent : IntegrationEvent
    {
        public Guid ProfileId { get; }
        public string Name { get; }
        public EmailAddress EmailAddress { get; }
        public DateTime BirthDate { get; }
        public string Bio { get; }
        public Address Address { get; }
        public PhoneNumber PhoneNumber { get; }
        public PhoneNumber MobileNumber { get; }

        public ProfileUpdatedInformationIntegrationEvent(Guid profileId, string name, EmailAddress emailAddress, DateTime birthDate, string bio, Address address, PhoneNumber phoneNumber, PhoneNumber mobileNumber)
        {
            ProfileId = profileId;
            Name = name;
            Address = address;
            BirthDate = birthDate;
            Bio = bio;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
        }
    }
}
