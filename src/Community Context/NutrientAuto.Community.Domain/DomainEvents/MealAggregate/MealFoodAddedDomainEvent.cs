using NutrientAuto.Shared.Events;
using System;

namespace NutrientAuto.Community.Domain.DomainEvents.MealAggregate
{
    public class MealFoodAddedDomainEvent : DomainEvent
    {
        public Guid MealId { get; }
        public Guid DietId { get; }
        public Guid FoodId { get; }

        public MealFoodAddedDomainEvent(Guid mealId, Guid dietId, Guid foodId)
        {
            MealId = mealId;
            DietId = dietId;
            FoodId = foodId;
        }
    }
}
