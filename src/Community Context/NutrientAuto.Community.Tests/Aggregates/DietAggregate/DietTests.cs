using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.DietAggregate;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Linq;

namespace NutrientAuto.Community.Tests.Aggregates.DietAggregate
{
    [TestClass]
    public class DietTests
    {
        #region Arrange
        public Diet GetNewDiet()
        {
            Guid profileId = Guid.NewGuid();
            Diet diet = new Diet(profileId, "Nova Dieta", "Dieta Low Carb");

            return diet;
        }
        #endregion

        #region Diet Meal
        [TestMethod]
        public void ShouldAddMealToDiet()
        {
            Diet diet = GetNewDiet();
            Meal meal = diet.AddMeal("Breakfast", "A faster breakfast", new Time(9, 0, 0));

            Assert.AreEqual(1, diet.MealCount);
        }

        [TestMethod]
        public void ShouldAddAndRemoveMealFromDiet()
        {
            Diet diet = GetNewDiet();
            Meal meal = diet.AddMeal("Breakfast", "A faster breakfast", new Time(9, 0, 0));

            diet.RemoveMeal(meal);

            Assert.AreEqual(0, diet.MealCount);
        }

        [TestMethod]
        public void ShouldFailToRemoveMealFromDietTwice()
        {
            Diet diet = GetNewDiet();
            Meal meal = diet.AddMeal("Breakfast", "A faster breakfast", new Time(9, 0, 0));

            diet.RemoveMeal(meal);
            diet.RemoveMeal(meal);

            Assert.IsFalse(diet.IsValid);
            Assert.AreEqual("A lista de refeições dessa dieta não contém essa refeição.", diet.GetNotifications().FirstOrDefault().Description);
        }
        #endregion

        #region Diet Macronutrient Calculation
        [TestMethod]
        public void ShouldRecalculateDietTotalMacros()
        {
            Diet diet = GetNewDiet();
            Meal meal = diet.AddMeal("Breakfast", "A faster breakfast", new Time(9, 0, 0));
            FoodUnit foodUnit = new FoodUnit(UnitType.Grams, 1);
            Food food = new Food("Peito de Frango", "Peito de Frango cozido", Guid.NewGuid(), new MacronutrientTable(0, 24, 2), MicronutrientTable.Default(), foodUnit);
            Food foodTwo = new Food("Brócolis", "Brócolis Verde", Guid.NewGuid(), new MacronutrientTable(6, 2, 0), MicronutrientTable.Default(), foodUnit);

            meal.AddMealFood(new MealFood(food, 1));
            meal.AddMealFood(new MealFood(foodTwo, 1));

            diet.RecalculateDietTotalMacros();

            Assert.AreEqual(6, diet.TotalMacronutrients.Carbohydrate);
            Assert.AreEqual(26, diet.TotalMacronutrients.Protein);
            Assert.AreEqual(2, diet.TotalMacronutrients.Fat);
        }

        [TestMethod]
        public void ShouldRecalculateDietTotalMacrosOnRemoveMeal()
        {
            Diet diet = GetNewDiet();
            Meal meal = diet.AddMeal("Breakfast", "A faster breakfast", new Time(9, 0, 0));
            Food food = new Food("Peito de Frango", "Peito de Frango cozido", Guid.NewGuid(), new MacronutrientTable(20, 48, 21), MicronutrientTable.Default(), new FoodUnit(UnitType.Grams, 1));

            meal.AddMealFood(new MealFood(food, 1));

            diet.RecalculateDietTotalMacros();

            Assert.AreEqual(20, diet.TotalMacronutrients.Carbohydrate);
            Assert.AreEqual(48, diet.TotalMacronutrients.Protein);
            Assert.AreEqual(21, diet.TotalMacronutrients.Fat);

            diet.RemoveMeal(meal);

            Assert.AreEqual(0, diet.TotalMacronutrients.Carbohydrate);
            Assert.AreEqual(0, diet.TotalMacronutrients.Protein);
            Assert.AreEqual(0, diet.TotalMacronutrients.Fat);
        }
        #endregion
    }
}
