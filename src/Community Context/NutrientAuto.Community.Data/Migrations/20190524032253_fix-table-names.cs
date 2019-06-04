using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class fixtablenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Profiles_FriendId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Friend_Profiles_ProfileId",
                table: "Friend");

            migrationBuilder.DropForeignKey(
                name: "FK_Measures_BodyPictures_Measures_MeasureId",
                table: "Measures_BodyPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLike_Posts_PostId",
                table: "PostLike");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLike_Profiles_ProfileId",
                table: "PostLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostLike",
                table: "PostLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Measures_BodyPictures",
                table: "Measures_BodyPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friend",
                table: "Friend");

            migrationBuilder.RenameTable(
                name: "PostLike",
                newName: "PostLikes");

            migrationBuilder.RenameTable(
                name: "Measures_BodyPictures",
                newName: "MeasureBodyPictures");

            migrationBuilder.RenameTable(
                name: "Friend",
                newName: "Friends");

            migrationBuilder.RenameIndex(
                name: "IX_PostLike_ProfileId",
                table: "PostLikes",
                newName: "IX_PostLikes_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PostLike_PostId",
                table: "PostLikes",
                newName: "IX_PostLikes_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Measures_BodyPictures_MeasureId",
                table: "MeasureBodyPictures",
                newName: "IX_MeasureBodyPictures_MeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_ProfileId",
                table: "Friends",
                newName: "IX_Friends_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_FriendId",
                table: "Friends",
                newName: "IX_Friends_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostLikes",
                table: "PostLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MeasureBodyPictures",
                table: "MeasureBodyPictures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friends",
                table: "Friends",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Profiles_FriendId",
                table: "Friends",
                column: "FriendId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Profiles_ProfileId",
                table: "Friends",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_MeasureBodyPictures_Measures_MeasureId",
                table: "MeasureBodyPictures",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLikes_Profiles_ProfileId",
                table: "PostLikes",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Profiles_FriendId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Profiles_ProfileId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_MeasureBodyPictures_Measures_MeasureId",
                table: "MeasureBodyPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Posts_PostId",
                table: "PostLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostLikes_Profiles_ProfileId",
                table: "PostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostLikes",
                table: "PostLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MeasureBodyPictures",
                table: "MeasureBodyPictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friends",
                table: "Friends");

            migrationBuilder.RenameTable(
                name: "PostLikes",
                newName: "PostLike");

            migrationBuilder.RenameTable(
                name: "MeasureBodyPictures",
                newName: "Measures_BodyPictures");

            migrationBuilder.RenameTable(
                name: "Friends",
                newName: "Friend");

            migrationBuilder.RenameIndex(
                name: "IX_PostLikes_ProfileId",
                table: "PostLike",
                newName: "IX_PostLike_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLike",
                newName: "IX_PostLike_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_MeasureBodyPictures_MeasureId",
                table: "Measures_BodyPictures",
                newName: "IX_Measures_BodyPictures_MeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_ProfileId",
                table: "Friend",
                newName: "IX_Friend_ProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_FriendId",
                table: "Friend",
                newName: "IX_Friend_FriendId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostLike",
                table: "PostLike",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Measures_BodyPictures",
                table: "Measures_BodyPictures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friend",
                table: "Friend",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Profiles_FriendId",
                table: "Friend",
                column: "FriendId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_Profiles_ProfileId",
                table: "Friend",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Measures_BodyPictures_Measures_MeasureId",
                table: "Measures_BodyPictures",
                column: "MeasureId",
                principalTable: "Measures",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLike_Posts_PostId",
                table: "PostLike",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PostLike_Profiles_ProfileId",
                table: "PostLike",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
