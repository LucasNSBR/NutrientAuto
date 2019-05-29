using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class macrosrename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MealProtein",
                table: "Meals",
                newName: "MealTotalProtein");

            migrationBuilder.RenameColumn(
                name: "MealKj",
                table: "Meals",
                newName: "MealTotalKj");

            migrationBuilder.RenameColumn(
                name: "MealKcal",
                table: "Meals",
                newName: "MealTotalKcal");

            migrationBuilder.RenameColumn(
                name: "MealFat",
                table: "Meals",
                newName: "MealTotalFat");

            migrationBuilder.RenameColumn(
                name: "MealCarbohydrate",
                table: "Meals",
                newName: "MealTotalCarbohydrate");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Protein",
                table: "MealFoods",
                newName: "MealFoodProtein");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Kj",
                table: "MealFoods",
                newName: "MealFoodKj");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Kcal",
                table: "MealFoods",
                newName: "MealFoodKcal");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Fat",
                table: "MealFoods",
                newName: "MealFoodFat");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Carbohydrate",
                table: "MealFoods",
                newName: "MealFoodCarbohydrate");

            migrationBuilder.RenameColumn(
                name: "FoodUnit_UnitType",
                table: "MealFoods",
                newName: "MealFoodUnitType");

            migrationBuilder.RenameColumn(
                name: "FoodUnit_DefaultGramsQuantityMultiplier",
                table: "MealFoods",
                newName: "MealFoodDefaultGramsQuantityMultiplier");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Protein",
                table: "Foods",
                newName: "FoodProtein");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Kj",
                table: "Foods",
                newName: "FoodKj");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Kcal",
                table: "Foods",
                newName: "FoodKcal");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Fat",
                table: "Foods",
                newName: "FoodFat");

            migrationBuilder.RenameColumn(
                name: "Macronutrients_Carbohydrate",
                table: "Foods",
                newName: "FoodCarbohydrate");

            migrationBuilder.RenameColumn(
                name: "FoodUnit_UnitType",
                table: "Foods",
                newName: "FoodUnitType");

            migrationBuilder.RenameColumn(
                name: "FoodUnit_DefaultGramsQuantityMultiplier",
                table: "Foods",
                newName: "FoodDefaultGramsQuantityMultiplier");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Zinc",
                table: "Foods",
                newName: "MicronutrientsZinc");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_VitaminE",
                table: "Foods",
                newName: "MicronutrientsVitaminE");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_VitaminD3",
                table: "Foods",
                newName: "MicronutrientsVitaminD3");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_VitaminC",
                table: "Foods",
                newName: "MicronutrientsVitaminC");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_VitaminB6",
                table: "Foods",
                newName: "MicronutrientsVitaminB6");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_VitaminB3",
                table: "Foods",
                newName: "MicronutrientsVitaminB3");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_VitaminB2",
                table: "Foods",
                newName: "MicronutrientsVitaminB2");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_VitaminB12",
                table: "Foods",
                newName: "MicronutrientsVitaminB12");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_VitaminB1",
                table: "Foods",
                newName: "MicronutrientsVitaminB1");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Sodium",
                table: "Foods",
                newName: "MicronutrientsSodium");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Potassium",
                table: "Foods",
                newName: "MicronutrientsPotassium");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Phosphorus",
                table: "Foods",
                newName: "MicronutrientsPhosphorus");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Manganese",
                table: "Foods",
                newName: "MicronutrientsManganese");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Magnesium",
                table: "Foods",
                newName: "MicronutrientsMagnesium");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Copper",
                table: "Foods",
                newName: "MicronutrientsCopper");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Chromium",
                table: "Foods",
                newName: "MicronutrientsChromium");

            migrationBuilder.RenameColumn(
                name: "Micronutrients_Calcium",
                table: "Foods",
                newName: "MicronutrientsCalcium");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MealTotalProtein",
                table: "Meals",
                newName: "MealProtein");

            migrationBuilder.RenameColumn(
                name: "MealTotalKj",
                table: "Meals",
                newName: "MealKj");

            migrationBuilder.RenameColumn(
                name: "MealTotalKcal",
                table: "Meals",
                newName: "MealKcal");

            migrationBuilder.RenameColumn(
                name: "MealTotalFat",
                table: "Meals",
                newName: "MealFat");

            migrationBuilder.RenameColumn(
                name: "MealTotalCarbohydrate",
                table: "Meals",
                newName: "MealCarbohydrate");

            migrationBuilder.RenameColumn(
                name: "MealFoodProtein",
                table: "MealFoods",
                newName: "Macronutrients_Protein");

            migrationBuilder.RenameColumn(
                name: "MealFoodKj",
                table: "MealFoods",
                newName: "Macronutrients_Kj");

            migrationBuilder.RenameColumn(
                name: "MealFoodKcal",
                table: "MealFoods",
                newName: "Macronutrients_Kcal");

            migrationBuilder.RenameColumn(
                name: "MealFoodFat",
                table: "MealFoods",
                newName: "Macronutrients_Fat");

            migrationBuilder.RenameColumn(
                name: "MealFoodCarbohydrate",
                table: "MealFoods",
                newName: "Macronutrients_Carbohydrate");

            migrationBuilder.RenameColumn(
                name: "MealFoodUnitType",
                table: "MealFoods",
                newName: "FoodUnit_UnitType");

            migrationBuilder.RenameColumn(
                name: "MealFoodDefaultGramsQuantityMultiplier",
                table: "MealFoods",
                newName: "FoodUnit_DefaultGramsQuantityMultiplier");

            migrationBuilder.RenameColumn(
                name: "FoodProtein",
                table: "Foods",
                newName: "Macronutrients_Protein");

            migrationBuilder.RenameColumn(
                name: "FoodKj",
                table: "Foods",
                newName: "Macronutrients_Kj");

            migrationBuilder.RenameColumn(
                name: "FoodKcal",
                table: "Foods",
                newName: "Macronutrients_Kcal");

            migrationBuilder.RenameColumn(
                name: "FoodFat",
                table: "Foods",
                newName: "Macronutrients_Fat");

            migrationBuilder.RenameColumn(
                name: "FoodCarbohydrate",
                table: "Foods",
                newName: "Macronutrients_Carbohydrate");

            migrationBuilder.RenameColumn(
                name: "FoodUnitType",
                table: "Foods",
                newName: "FoodUnit_UnitType");

            migrationBuilder.RenameColumn(
                name: "FoodDefaultGramsQuantityMultiplier",
                table: "Foods",
                newName: "FoodUnit_DefaultGramsQuantityMultiplier");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsZinc",
                table: "Foods",
                newName: "Micronutrients_Zinc");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsVitaminE",
                table: "Foods",
                newName: "Micronutrients_VitaminE");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsVitaminD3",
                table: "Foods",
                newName: "Micronutrients_VitaminD3");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsVitaminC",
                table: "Foods",
                newName: "Micronutrients_VitaminC");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsVitaminB6",
                table: "Foods",
                newName: "Micronutrients_VitaminB6");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsVitaminB3",
                table: "Foods",
                newName: "Micronutrients_VitaminB3");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsVitaminB2",
                table: "Foods",
                newName: "Micronutrients_VitaminB2");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsVitaminB12",
                table: "Foods",
                newName: "Micronutrients_VitaminB12");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsVitaminB1",
                table: "Foods",
                newName: "Micronutrients_VitaminB1");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsSodium",
                table: "Foods",
                newName: "Micronutrients_Sodium");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsPotassium",
                table: "Foods",
                newName: "Micronutrients_Potassium");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsPhosphorus",
                table: "Foods",
                newName: "Micronutrients_Phosphorus");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsManganese",
                table: "Foods",
                newName: "Micronutrients_Manganese");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsMagnesium",
                table: "Foods",
                newName: "Micronutrients_Magnesium");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsCopper",
                table: "Foods",
                newName: "Micronutrients_Copper");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsChromium",
                table: "Foods",
                newName: "Micronutrients_Chromium");

            migrationBuilder.RenameColumn(
                name: "MicronutrientsCalcium",
                table: "Foods",
                newName: "Micronutrients_Calcium");
        }
    }
}
