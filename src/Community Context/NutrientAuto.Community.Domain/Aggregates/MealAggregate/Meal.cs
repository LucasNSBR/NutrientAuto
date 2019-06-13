using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutrientAuto.Community.Domain.Aggregates.MealAggregate
{
    public class Meal : Entity<Meal>, IAggregateRoot, IProfileEntity
    {
        public Guid ProfileId { get; private set; }

        public Guid DietId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Time TimeOfDay { get; private set; }
        public MacronutrientTable MealMacronutrients { get; private set; }

        public int MealFoodCount => _mealFoods?.Count ?? 0;

        private readonly List<MealFood> _mealFoods = new List<MealFood>();
        public IReadOnlyList<MealFood> MealFoods => _mealFoods;

        protected Meal()
        {
        }

        public Meal(Guid profileId, Guid dietId, string name, string description, Time timeOfDay)
        {
            ProfileId = profileId;
            DietId = dietId;
            Name = name;
            Description = description;
            TimeOfDay = timeOfDay;

            MealMacronutrients = MacronutrientTable.Default();
            _mealFoods = new List<MealFood>();
        }

        public void Update(string name, string description, Time timeOfDay)
        {
            Name = name;
            Description = description;
            TimeOfDay = timeOfDay;
        }

        public MealFood FindMealFood(Guid mealFoodId)
        {
            return _mealFoods.Find(mealFood => mealFood.Id == mealFoodId);
        }

        public void AddMealFood(MealFood mealFood)
        {
            if (_mealFoods.Contains(mealFood))
            {
                AddNotification("Alimento duplicado", "Essa refeição já contém esse mesmo alimento.");
                return;
            }

            _mealFoods.Add(mealFood);
            RecalculateMealTotalMacros();
        }

        public void RemoveMealFood(MealFood mealFood)
        {
            if (!_mealFoods.Contains(mealFood))
            {
                AddNotification("Alimento indisponível", "A lista de alimentos dessa refeição não contém esse alimento.");
                return;
            }

            _mealFoods.Remove(mealFood);
            RecalculateMealTotalMacros();
        }

        private void RecalculateMealTotalMacros()
        {
            MealMacronutrients = MacronutrientTable.Default();

            if (!_mealFoods.Any())
                MealMacronutrients = MacronutrientTable.Default();

            foreach (MealFood mealFood in _mealFoods)
            {
                MealMacronutrients = MealMacronutrients
                    .Sum(mealFood.Macronutrients);
            }
        }
    }
}
