using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class measurelinerename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasureLine_MeasureCategories_MeasureCategoryId",
                table: "MeasureLine");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasureLine_Measures_MeasureId",
                table: "MeasureLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasureLine",
                table: "MeasureLine");

            migrationBuilder.RenameTable(
                name: "MeasureLine",
                newName: "MeasureLines");

            migrationBuilder.RenameIndex(
                name: "IX_MeasureLine_MeasureId",
                table: "MeasureLines",
                newName: "IX_MeasureLines_MeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasureLine_MeasureCategoryId",
                table: "MeasureLines",
                newName: "IX_MeasureLines_MeasureCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasureLines",
                table: "MeasureLines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasureLines_MeasureCategories_MeasureCategoryId",
                table: "MeasureLines",
                column: "MeasureCategoryId",
                principalTable: "MeasureCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasureLines_Measures_MeasureId",
                table: "MeasureLines",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasureLines_MeasureCategories_MeasureCategoryId",
                table: "MeasureLines");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasureLines_Measures_MeasureId",
                table: "MeasureLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasureLines",
                table: "MeasureLines");

            migrationBuilder.RenameTable(
                name: "MeasureLines",
                newName: "MeasureLine");

            migrationBuilder.RenameIndex(
                name: "IX_MeasureLines_MeasureId",
                table: "MeasureLine",
                newName: "IX_MeasureLine_MeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasureLines_MeasureCategoryId",
                table: "MeasureLine",
                newName: "IX_MeasureLine_MeasureCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasureLine",
                table: "MeasureLine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasureLine_MeasureCategories_MeasureCategoryId",
                table: "MeasureLine",
                column: "MeasureCategoryId",
                principalTable: "MeasureCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasureLine_Measures_MeasureId",
                table: "MeasureLine",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
