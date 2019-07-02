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
                    Bio = table.Column<string>(maxLength: 500, nullable: true),
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
                    DietTotalKcal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DietTotalKj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DietTotalProtein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DietTotalCarbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DietTotalFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FriendId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friends_Profiles_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friends_Profiles_UserId",
                        column: x => x.UserId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FriendshipRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    RequesterId = table.Column<Guid>(nullable: false),
                    RequestedId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    RequestBody = table.Column<string>(maxLength: 250, nullable: true),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendshipRequests_Profiles_RequestedId",
                        column: x => x.RequestedId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FriendshipRequests_Profiles_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bmi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    PostImageUrlPath = table.Column<string>(maxLength: 500, nullable: true),
                    PostImageName = table.Column<string>(maxLength: 150, nullable: true),
                    HasEntityReference = table.Column<bool>(nullable: false),
                    EntityReferenceId = table.Column<Guid>(nullable: true),
                    EntityReferenceType = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
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
                    MealTotalKcal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealTotalKj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealTotalProtein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealTotalCarbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealTotalFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    FoodKcal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodKj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodProtein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodCarbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MicronutrientsCalcium = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsChromium = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsCopper = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsMagnesium = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsManganese = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsPhosphorus = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsPotassium = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsSodium = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsSelenium = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsZinc = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsVitaminB1 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsVitaminB2 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsVitaminB3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsVitaminB6 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsVitaminB12 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsVitaminC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsVitaminD3 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MicronutrientsVitaminE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FoodUnitType = table.Column<int>(nullable: false),
                    FoodDefaultGramsQuantityMultiplier = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                name: "MeasureBodyPictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BodyPictureImageUrlPath = table.Column<string>(maxLength: 500, nullable: false),
                    BodyPictureImageName = table.Column<string>(maxLength: 150, nullable: false),
                    MeasureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureBodyPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasureBodyPictures_Measures_MeasureId",
                        column: x => x.MeasureId,
                        principalTable: "Measures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasureLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MeasureCategoryId = table.Column<Guid>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasureLines_MeasureCategories_MeasureCategoryId",
                        column: x => x.MeasureCategoryId,
                        principalTable: "MeasureCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeasureLines_Measures_MeasureId",
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ReplyTo",
                        column: x => x.ReplyTo,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLikes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLikes_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MealFoods",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FoodId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealFoodKcal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealFoodKj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealFoodProtein = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealFoodCarbohydrate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealFoodFat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MealFoods_Meals_MealId",
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
                name: "IX_Friends_FriendId",
                table: "Friends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UserId",
                table: "Friends",
                column: "UserId");

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
                name: "IX_MealFoods_FoodId",
                table: "MealFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_MealFoods_MealId",
                table: "MealFoods",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DietId",
                table: "Meals",
                column: "DietId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureBodyPictures_MeasureId",
                table: "MeasureBodyPictures",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureCategories_ProfileId",
                table: "MeasureCategories",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureLines_MeasureCategoryId",
                table: "MeasureLines",
                column: "MeasureCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasureLines_MeasureId",
                table: "MeasureLines",
                column: "MeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Measures_ProfileId",
                table: "Measures",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLikes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_ProfileId",
                table: "PostLikes",
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
                name: "Friends");

            migrationBuilder.DropTable(
                name: "FriendshipRequests");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "MealFoods");

            migrationBuilder.DropTable(
                name: "MeasureBodyPictures");

            migrationBuilder.DropTable(
                name: "MeasureLines");

            migrationBuilder.DropTable(
                name: "PostLikes");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "MeasureCategories");

            migrationBuilder.DropTable(
                name: "Measures");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "FoodTables");

            migrationBuilder.DropTable(
                name: "Diets");

            migrationBuilder.DropTable(
                name: "Profiles");
        }
    }
}
