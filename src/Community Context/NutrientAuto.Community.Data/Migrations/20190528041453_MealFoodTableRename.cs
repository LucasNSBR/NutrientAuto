using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class MealFoodTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealFood_Meals_MealId",
                table: "MealFood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealFood",
                table: "MealFood");

            migrationBuilder.RenameTable(
                name: "MealFood",
                newName: "MealFoods");

            migrationBuilder.RenameIndex(
                name: "IX_MealFood_MealId",
                table: "MealFoods",
                newName: "IX_MealFoods_MealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealFoods",
                table: "MealFoods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealFoods_Meals_MealId",
                table: "MealFoods",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealFoods_Meals_MealId",
                table: "MealFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealFoods",
                table: "MealFoods");

            migrationBuilder.RenameTable(
                name: "MealFoods",
                newName: "MealFood");

            migrationBuilder.RenameIndex(
                name: "IX_MealFoods_MealId",
                table: "MealFood",
                newName: "IX_MealFood_MealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealFood",
                table: "MealFood",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MealFood_Meals_MealId",
                table: "MealFood",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
