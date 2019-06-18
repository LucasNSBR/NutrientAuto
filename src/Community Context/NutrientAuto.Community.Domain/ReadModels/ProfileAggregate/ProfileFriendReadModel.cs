using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.ProfileAggregate
{
    public class ProfileFriendReadModel
    {
        public Guid ProfileId { get; set; }
        public string Name { get; set; }
        public Image AvatarImage { get; set; }
    }
}
