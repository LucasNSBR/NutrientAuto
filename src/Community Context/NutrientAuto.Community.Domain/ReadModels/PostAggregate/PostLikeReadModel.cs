using System;

namespace NutrientAuto.Community.Domain.ReadModels.PostAggregate
{
    public class PostLikeReadModel
    {
        public Guid ProfileId { get; set; }
        public DateTime DateCreated { get; set; }
        public string ProfileName { get; set; }
    }
}
