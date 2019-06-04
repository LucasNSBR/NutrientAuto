using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NutrientAuto.Community.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AvatarImageUrlPath = table.Column<string>(maxLength: 500, nullable: false),
                    AvatarImageName = table.Column<string>(maxLength: 150, nullable: false),
                    Genre = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Username = table.Column<string>(maxLength: 30, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 250, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Bio = table.Column<string>(maxLength: 500, nullable: false),
                    PrivacyType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    TotalMacronutrients_Kcal = table.Column<decimal>(nullable: false),
                    TotalMacronutrients_Kj = table.Column<decimal>(nullable: false),
                    TotalMacronutrients_Protein = table.Column<decimal>(nullable: false),
                    TotalMacronutrients_Carbohydrate = table.Column<decimal>(nullable: false),
                    TotalMacronutrients_Fat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diets_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FoodTableType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    ProfileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodTables_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friend",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FriendId = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friend_Profiles_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Friend_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FriendshipRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequesterId = table.Column<Guid>(nullable: false),
                    RequestedId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    RequestBody = table.Column<string>(maxLength: 250, nullable: true),
                    AcceptedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendshipRequests_Profiles_RequestedId",
                        column: x => x.RequestedId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_FriendshipRequests_Profiles_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    DateCompleted = table.Column<DateTime>(nullable: true),
                    AccomplishmentDetails = table.Column<string>(maxLength: 500, nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Details = table.Column<string>(maxLength: 500, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasureCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MeasureCategoryType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    IsFavorite = table.Column<bool>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasureCategories_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Details = table.Column<string>(maxLength: 250, nullable: false),
                    Height = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Bmi = table.Column<decimal>(nullable: false),
                    MeasureDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measures_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Body = table.Column<string>(maxLength: 250, nullable: false),
                    PostImageUrlPath = table.Column<string>(maxLength: 500, nullable: false),
                    PostImageName = table.Column<string>(maxLength: 150, nullable: false),
                    HasEntityReference = table.Column<bool>(nullable: false),
                    EntityReferenceId = table.Column<Guid>(nullable: true),
                    EntityReferenceType = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PostType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Details = table.Column<string>(maxLength: 250, nullable: false),
                    TimeOfDay_Hour = table.Column<int>(nullable: false),
                    TimeOfDay_Minute = table.Column<int>(nullable: false),
                    TimeOfDay_Second = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    DietId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    MealHour = table.Column<int>(nullable: false),
                    MealMinute = table.Column<int>(nullable: false),
                    MealSecond = table.Column<int>(nullable: false),
                    MealKcal = table.Column<decimal>(nullable: false),
                    MealKj = table.Column<decimal>(nullable: false),
                    MealProtein = table.Column<decimal>(nullable: false),
                    MealCarbohydrate = table.Column<decimal>(nullable: false),
                    MealFat = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FoodType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    FoodTableId = table.Column<Guid>(nullable: false),
                    Macronutrients_Kcal = table.Column<decimal>(nullable: false),
                    Macronutrients_Kj = table.Column<decimal>(nullable: false),
                    Macronutrients_Protein = table.Column<decimal>(nullable: false),
                    Macronutrients_Carbohydrate = table.Column<decimal>(nullable: false),
                    Macronutrients_Fat = table.Column<decimal>(nullable: false),
                    Micronutrients_Calcium = table.Column<decimal>(nullable: true),
                    Micronutrients_Chromium = table.Column<decimal>(nullable: true),
                    Micronutrients_Copper = table.Column<decimal>(nullable: true),
                    Micronutrients_Magnesium = table.Column<decimal>(nullable: true),
                    Micronutrients_Manganese = table.Column<decimal>(nullable: true),
                    Micronutrients_Phosphorus = table.Column<decimal>(nullable: true),
                    Micronutrients_Potassium = table.Column<decimal>(nullable: true),
                    Micronutrients_Sodium = table.Column<decimal>(nullable: true),
                    Micronutrients_Selenium = table.Column<decimal>(nullable: true),
                    Micronutrients_Zinc = table.Column<decimal>(nullable: true),
                    Micronutrients_VitaminB1 = table.Column<decimal>(nullable: true),
                    Micronutrients_VitaminB2 = table.Column<decimal>(nullable: true),
                    Micronutrients_VitaminB3 = table.Column<decimal>(nullable: true),
                    Micronutrients_VitaminB6 = table.Column<decimal>(nullable: true),
                    Micronutrients_VitaminB12 = table.Column<decimal>(nullable: true),
                    Micronutrients_VitaminC = table.Column<decimal>(nullable: true),
                    Micronutrients_VitaminD3 = table.Column<decimal>(nullable: true),
                    Micronutrients_VitaminE = table.Column<decimal>(nullable: true),
                    FoodUnit_UnitType = table.Column<int>(nullable: false),
                    FoodUnit_DefaultGramsQuantityMultiplier = table.Column<decimal>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foods_FoodTables_FoodTableId",
                        column: x => x.FoodTableId,
                        principalTable: "FoodTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MeasureLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MeasureCategoryId = table.Column<Guid>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    MeasureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasureLine_MeasureCategories_MeasureCategoryId",
                        column: x => x.MeasureCategoryId,
                        principalTable: "MeasureCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MeasureLine_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Measures_BodyPictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BodyPictureImageUrlPath = table.Column<string>(maxLength: 500, nullable: false),
                    BodyPictureImageName = table.Column<string>(maxLength: 150, nullable: false),
                    MeasureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measures_BodyPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measures_BodyPictures_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(maxLength: 150, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ReplyTo = table.Column<Guid>(nullable: true),
                    PostId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId1",
                        column: x => x.PostId1,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ReplyTo",
                        column: x => x.ReplyTo,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLike_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLike_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MealFood",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FoodId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    FoodUnit_UnitType = table.Column<int>(nullable: false),
                    FoodUnit_DefaultGramsQuantityMultiplier = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Macronutrients_Kcal = table.Column<decimal>(nullable: false),
                    Macronutrients_Kj = table.Column<decimal>(nullable: false),
                    Macronutrients_Protein = table.Column<decimal>(nullable: false),
                    Macronutrients_Carbohydrate = table.Column<decimal>(nullable: false),
                    Macronutrients_Fat = table.Column<decimal>(nullable: false),
                    MealId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealFood_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId1",
                table: "Comments",
                column: "PostId1");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProfileId",
                table: "Comments",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReplyTo",
                table: "Comments",
                column: "ReplyTo");

            migrationBuilder.CreateIndex(
                name: "IX_Diets_ProfileId",
                table: "Diets",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ProfileId",
                table: "Foods",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodTableId",
                table: "Foods",
                column: "FoodTableId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTables_ProfileId",
                table: "FoodTables",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_FriendId",
                table: "Friend",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friend_ProfileId",
                table: "Friend",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipRequests_RequestedId",
                table: "FriendshipRequests",
                column: "RequestedId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipRequests_RequesterId",
                table: "FriendshipRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ProfileId",
                table: "Goals",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MealFood_MealId",
                table: "MealFood",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DietId",
                table: "Meals",
                column: "DietId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureCategories_ProfileId",
                table: "MeasureCategories",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureLine_MeasureCategoryId",
                table: "MeasureLine",
                column: "MeasureCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureLine_MeasureId",
                table: "MeasureLine",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_ProfileId",
                table: "Measures",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_BodyPictures_MeasureId",
                table: "Measures_BodyPictures",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLike_PostId",
                table: "PostLike",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLike_ProfileId",
                table: "PostLike",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ProfileId",
                table: "Posts",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_ProfileId",
                table: "Reminders",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Friend");

            migrationBuilder.DropTable(
                name: "FriendshipRequests");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "MealFood");

            migrationBuilder.DropTable(
                name: "MeasureLine");

            migrationBuilder.DropTable(
                name: "Measures_BodyPictures");

            migrationBuilder.DropTable(
                name: "PostLike");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "FoodTables");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "MeasureCategories");

            migrationBuilder.DropTable(
                name: "Measures");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Diets");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
