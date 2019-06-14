using System;

namespace NutrientAuto.Community.Domain.ReadModels.PostAggregate
{
    public class ReplyReadModel
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid ProfileId { get; set; }
        public string ProfileName { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ReplyTo { get; set; }
    }
}
