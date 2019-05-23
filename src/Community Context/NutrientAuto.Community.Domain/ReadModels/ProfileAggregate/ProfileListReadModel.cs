using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.ProfileAggregate
{
    public class ProfileListReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        public Image AvatarImage { get; set; }
    }
}
