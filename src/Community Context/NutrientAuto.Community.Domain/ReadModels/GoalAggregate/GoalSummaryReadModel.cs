using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.GoalAggregate
{
    public class GoalSummaryReadModel
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime DateCreated { get; set; }

        public GoalStatus GoalStatus { get; set; }
    }
}
