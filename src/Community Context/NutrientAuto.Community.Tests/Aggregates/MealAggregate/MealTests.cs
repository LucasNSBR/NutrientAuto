using Microsoft.VisualStudio.TestTools.UnitTesting;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Linq;

namespace NutrientAuto.Community.Tests.Aggregates.MealAggregate
{
    [TestClass]
    public class MealTests
    {
        #region Arrange
        public Meal GetMeal()
        {
            Guid profileId = Guid.NewGuid();
            Guid dietId = Guid.NewGuid();

            Meal meal = new Meal(profileId, dietId, "Breakfast", "Breakfaster", new Time(9, 0, 0));
            return meal;
        }
        #endregion

        #region MealFood
        [TestMethod]
        public void ShouldAddMealFoodToMeal()
        {
            Meal meal = GetMeal();
            FoodUnit foodUnit = new FoodUnit(UnitType.Grams, 1);

            Food food = new Food("Bacon", "Bacon de testes", Guid.NewGuid(), MacronutrientTable.Default(), MicronutrientTable.Default(), foodUnit);
            meal.AddMealFood(new MealFood(food, 1));

            Assert.AreEqual(1, meal.MealFoodCount);
        }

        [TestMethod]
        [TestCategory("Meal MealFood")]
        public void ShouldFailToAddMealFoodToMealTwice()
        {
            Meal meal = GetMeal();
            FoodUnit foodUnit = new FoodUnit(UnitType.Grams, 1);

            Food food = new Food("Bacon", "Bacon de testes", Guid.NewGuid(), MacronutrientTable.Default(), MicronutrientTable.Default(), foodUnit);
            MealFood mealFood = new MealFood(food, 1);

            meal.AddMealFood(mealFood);
            meal.AddMealFood(mealFood);

            Assert.IsFalse(meal.IsValid);
        }

        [TestMethod]
        [TestCategory("Meal MealFood")]
        public void ShouldAddAndRemoveMealFoodFromMeal()
        {
            Meal meal = GetMeal();
            FoodUnit foodUnit = new FoodUnit(UnitType.Grams, 1);

            Food food = new Food("Bacon", "Bacon de testes", Guid.NewGuid(), MacronutrientTable.Default(), MicronutrientTable.Default(), foodUnit);
            MealFood mealFood = new MealFood(food, 0);

            meal.AddMealFood(mealFood);
            Assert.AreEqual(1, meal.MealFoodCount);

            meal.RemoveMealFood(mealFood);
            Assert.AreEqual(0, meal.MealFoodCount);
        }

        [TestMethod]
        public void ShouldFailToRemoveMealFoodFromMealTwice()
        {
            Meal meal = GetMeal();
            FoodUnit foodUnit = new FoodUnit(UnitType.Grams, 1);

            Food food = new Food("Bacon", "Bacon de testes", Guid.NewGuid(), MacronutrientTable.Default(), MicronutrientTable.Default(), foodUnit);
            MealFood mealFood = new MealFood(food, 0);

            meal.RemoveMealFood(mealFood);
            meal.RemoveMealFood(mealFood);

            Assert.IsFalse(meal.IsValid);
            Assert.AreEqual("A lista de alimentos dessa refeição não contém esse alimento.", meal.GetNotifications().FirstOrDefault().Description);
        }

        [TestMethod]
        public void ShouldRecalculateMealMacros()
        {
            Meal meal = GetMeal();
            FoodUnit foodUnit = new FoodUnit(UnitType.Grams, 1);

            Food food = new Food("Peito de Frango", "Peito de Frango cozido", Guid.NewGuid(), new MacronutrientTable(0, 24, 2), MicronutrientTable.Default(), foodUnit);
            Food foodTwo = new Food("Brócolis", "Brócolis Verde", Guid.NewGuid(), new MacronutrientTable(6, 2, 0), MicronutrientTable.Default(), foodUnit);

            meal.AddMealFood(new MealFood(food, 1));
            meal.AddMealFood(new MealFood(foodTwo, 1));

            Assert.AreEqual(6, meal.MealMacronutrients.Carbohydrate);
            Assert.AreEqual(26, meal.MealMacronutrients.Protein);
            Assert.AreEqual(2, meal.MealMacronutrients.Fat);
        }

        [TestMethod]
        public void ShouldRecalculateMealMacrosOnRemoveMealFood()
        {
            Meal meal = GetMeal();
            FoodUnit foodUnit = new FoodUnit(UnitType.Grams, 100);

            Food food = new Food("Peito de Frango", "Peito de Frango cozido", Guid.NewGuid(), new MacronutrientTable(0, 24, 2), MicronutrientTable.Default(), foodUnit);
            Food foodTwo = new Food("Brócolis", "Brócolis Verde", Guid.NewGuid(), new MacronutrientTable(6, 2, 0), MicronutrientTable.Default(), foodUnit);

            meal.AddMealFood(new MealFood(food, 100));
            meal.AddMealFood(new MealFood(foodTwo, 100));

            Assert.AreEqual(6, meal.MealMacronutrients.Carbohydrate);
            Assert.AreEqual(26, meal.MealMacronutrients.Protein);
            Assert.AreEqual(2, meal.MealMacronutrients.Fat);

            meal.RemoveMealFood(meal.MealFoods[0]);
            meal.RemoveMealFood(meal.MealFoods[0]);

            Assert.AreEqual(0, meal.MealMacronutrients.Carbohydrate);
            Assert.AreEqual(0, meal.MealMacronutrients.Protein);
            Assert.AreEqual(0, meal.MealMacronutrients.Fat);
        }
        #endregion
    }
}
