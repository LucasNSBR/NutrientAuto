using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class microsrename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Micronutrients_Selenium",
                table: "Foods",
                newName: "MicronutrientsSelenium");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MicronutrientsSelenium",
                table: "Foods",
                newName: "Micronutrients_Selenium");
        }
    }
}
