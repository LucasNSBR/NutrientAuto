using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class dietmacro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalMacronutrients_Protein",
                table: "Diets",
                newName: "DietTotalProtein");

            migrationBuilder.RenameColumn(
                name: "TotalMacronutrients_Kj",
                table: "Diets",
                newName: "DietTotalKj");

            migrationBuilder.RenameColumn(
                name: "TotalMacronutrients_Kcal",
                table: "Diets",
                newName: "DietTotalKcal");

            migrationBuilder.RenameColumn(
                name: "TotalMacronutrients_Fat",
                table: "Diets",
                newName: "DietTotalFat");

            migrationBuilder.RenameColumn(
                name: "TotalMacronutrients_Carbohydrate",
                table: "Diets",
                newName: "DietTotalCarbohydrate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DietTotalProtein",
                table: "Diets",
                newName: "TotalMacronutrients_Protein");

            migrationBuilder.RenameColumn(
                name: "DietTotalKj",
                table: "Diets",
                newName: "TotalMacronutrients_Kj");

            migrationBuilder.RenameColumn(
                name: "DietTotalKcal",
                table: "Diets",
                newName: "TotalMacronutrients_Kcal");

            migrationBuilder.RenameColumn(
                name: "DietTotalFat",
                table: "Diets",
                newName: "TotalMacronutrients_Fat");

            migrationBuilder.RenameColumn(
                name: "DietTotalCarbohydrate",
                table: "Diets",
                newName: "TotalMacronutrients_Carbohydrate");
        }
    }
}
