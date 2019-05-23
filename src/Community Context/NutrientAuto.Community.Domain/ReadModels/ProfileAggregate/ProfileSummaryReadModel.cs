using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.ProfileAggregate
{
    public class ProfileSummaryReadModel
    {
        public Guid Id { get; set; }
        public Genre Genre { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }

        public EmailAddress EmailAddress { get; set; }
        public Image AvatarImage { get; set; }
        public PrivacyType PrivacyType { get; set; }

        public int FriendsCount { get; set; }
    }
}
