using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class friendshiprequeststatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostType",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AcceptedDate",
                table: "FriendshipRequests");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "FriendshipRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FriendshipRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Friends",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "FriendshipRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FriendshipRequests");

            migrationBuilder.AddColumn<int>(
                name: "PostType",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedDate",
                table: "FriendshipRequests",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Friends",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
