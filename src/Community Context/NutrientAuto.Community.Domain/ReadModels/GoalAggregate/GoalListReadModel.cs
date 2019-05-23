using System;

namespace NutrientAuto.Community.Domain.ReadModels.GoalAggregate
{
    public class GoalListReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
    }
}
