using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.PostAggregate
{
    public class PostListReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }

        public Image AttachedImage { get; set; }

        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
    }
}
