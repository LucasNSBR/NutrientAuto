using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.ReadModels.PostAggregate
{
    public class CommentReadModel 
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid ProfileId { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }

        public List<ReplyReadModel> Replies { get; set; } 
    }
}
