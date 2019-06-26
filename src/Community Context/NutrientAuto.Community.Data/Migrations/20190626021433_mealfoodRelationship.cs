using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class mealfoodRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MealFoods");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MealFoods");

            migrationBuilder.DropColumn(
                name: "MealFoodDefaultGramsQuantityMultiplier",
                table: "MealFoods");

            migrationBuilder.DropColumn(
                name: "MealFoodUnitType",
                table: "MealFoods");

            migrationBuilder.CreateIndex(
                name: "IX_MealFoods_FoodId",
                table: "MealFoods",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealFoods_Foods_FoodId",
                table: "MealFoods",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealFoods_Foods_FoodId",
                table: "MealFoods");

            migrationBuilder.DropIndex(
                name: "IX_MealFoods_FoodId",
                table: "MealFoods");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MealFoods",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MealFoods",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MealFoodDefaultGramsQuantityMultiplier",
                table: "MealFoods",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MealFoodUnitType",
                table: "MealFoods",
                nullable: false,
                defaultValue: 0);
        }
    }
}
