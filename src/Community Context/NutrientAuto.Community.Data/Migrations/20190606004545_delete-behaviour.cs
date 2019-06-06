using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class deletebehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ReplyTo",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ReplyTo",
                table: "Comments",
                column: "ReplyTo",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ReplyTo",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ReplyTo",
                table: "Comments",
                column: "ReplyTo",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
