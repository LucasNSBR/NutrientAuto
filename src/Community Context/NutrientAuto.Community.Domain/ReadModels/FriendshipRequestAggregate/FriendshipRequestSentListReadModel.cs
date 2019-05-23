using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.FriendshipRequestAggregate
{
    public class FriendshipRequestSentListReadModel
    {
        public Guid Id { get; set; }
        public Guid RequestedId { get; set; }
        public DateTime DateCreated { get; set; }
        public Image RequestedAvatarImage { get; set; }
        public string RequestedName { get; set; }
    }
}
