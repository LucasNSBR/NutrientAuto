using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class onwsonedelete : Migration
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
                onDelete: ReferentialAction.Restrict);
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
