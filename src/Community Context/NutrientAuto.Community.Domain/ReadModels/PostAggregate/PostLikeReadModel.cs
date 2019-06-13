using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.PostAggregate
{
    public class PostLikeReadModel
    {
        public Guid ProfileId { get; set; }
        public Image ProfileAvatarImage { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
