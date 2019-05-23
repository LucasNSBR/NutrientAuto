using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.FriendshipRequestAggregate
{
    public class FriendshipRequestListReadModel
    {
        public Guid Id { get; set; }
        public Guid RequesterId { get; set; }
        public DateTime DateCreated { get; set; }
        public Image RequesterAvatarImage { get; set; }
        public string RequesterName { get; set; }
    }
}
