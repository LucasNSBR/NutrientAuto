using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.ReadModels.PostAggregate
{
    public class PostSummaryReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }

        public Image AttachedImage { get; set; }

        public EntityReference EntityReference { get; set; }

        public List<PostLike> Likes { get; set; }
        public List<CommentReadModel> Comments { get; set; } 
    }
}
