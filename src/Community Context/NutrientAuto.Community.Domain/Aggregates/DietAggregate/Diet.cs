using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NutrientAuto.Community.Domain.Aggregates.DietAggregate
{
    public class Diet : Entity<Diet>, IAggregateRoot, IProfileEntity
    {
        public Guid ProfileId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime StartDate { get; private set; }

        public MacronutrientTable TotalMacronutrients { get; private set; }

        private readonly List<Meal> _dietMeals;
        public IReadOnlyList<Meal> DietMeals => _dietMeals;

        public int MealCount => _dietMeals.Count;

        public Diet(Guid profileId, string name, string description)
        {
            ProfileId = profileId;
            Name = name;
            Description = description;
            StartDate = DateTime.Now;
            TotalMacronutrients = MacronutrientTable.Default();
            _dietMeals = new List<Meal>();
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Meal FindMeal(Guid mealId)
        {
            return _dietMeals.Find(meal => meal.Id == mealId);
        }

        public Meal AddMeal(string name, string description, Time timeOfDay)
        {
            Guid dietId = Id;

            Meal dietMeal = new Meal(ProfileId, dietId, name, description, timeOfDay);
            _dietMeals.Add(dietMeal);

            RecalculateDietTotalMacros();

            return dietMeal;
        }

        public void RemoveMeal(Meal dietMeal)
        {
            if (!_dietMeals.Contains(dietMeal))
            {
                AddNotification("Refeição indisponível", "A lista de refeições dessa dieta não contém essa refeição.");
                return;
            }

            _dietMeals.Remove(dietMeal);
            RecalculateDietTotalMacros();
        }

        public void RecalculateDietTotalMacros()
        {
            TotalMacronutrients = MacronutrientTable.Default();

            if (!_dietMeals.Any())
                TotalMacronutrients = MacronutrientTable.Default();

            foreach (Meal dietMeal in _dietMeals)
            {
                TotalMacronutrients = TotalMacronutrients
                    .Sum(dietMeal.MealMacronutrients);
            }
        }
    }
}
