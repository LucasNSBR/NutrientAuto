using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.ProfileAggregate
{
    public class ProfileOverviewReadModel
    {
        public Guid Id { get; }
        public Genre Genre { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }

        public EmailAddress EmailAddress { get; set; }
        public Image AvatarImage { get; set; }
    }
}
