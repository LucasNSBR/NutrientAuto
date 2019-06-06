using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class friendentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Profiles_ProfileId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_ProfileId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Friends");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendId",
                table: "Friends",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Profiles_FriendId",
                table: "Friends",
                column: "FriendId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Profiles_FriendId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_FriendId",
                table: "Friends");

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "Friends",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ProfileId",
                table: "Friends",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Profiles_ProfileId",
                table: "Friends",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
