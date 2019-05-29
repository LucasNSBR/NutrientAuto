using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.ReadModels.MealAggregate;
using NutrientAuto.Community.Domain.Repositories.MealAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.MealAggregate
{
    public class MealReadModelRepository : BaseReadModelRepository, IMealReadModelRepository
    {
        public MealReadModelRepository(CommunityDbContext dbContext)
               : base(dbContext)
        {
        }

        public async Task<MealSummaryReadModel> GetMealSummaryAsync(Guid id)
        {
            string sql = $@"SELECT Meals.Id, Meals.ProfileId, Meals.DietId,
                         Meals.Name, Meals.MealHour AS Hour, Meals.MealMinute AS Minute, Meals.MealSecond AS Second, 
                         Meals.MealTotalKcal AS Kcal, Meals.MealTotalKj AS Kj, Meals.MealTotalProtein AS Protein, Meals.MealTotalCarbohydrate AS Carbohydrate, Meals.MealTotalFat AS Fat
                         FROM Meals
                         WHERE Id = @id;

                         SELECT MealFoods.Id, MealFoods.FoodId, MealFoods.Name, MealFoods.Description, MealFoods.Quantity,
                         MealFoods.MealFoodUnitType AS UnitType, MealFoods.MealFoodDefaultGramsQuantityMultiplier AS DefaultGramsQuantityMultiplier,
                         MealFoods.MealFoodKcal AS Kcal, MealFoods.MealFoodKj AS Kj, MealFoods.MealFoodProtein AS Protein, MealFoods.MealFoodCarbohydrate AS Carbohydrate, MealFoods.MealFoodFat AS Fat
                         FROM MealFoods
                         WHERE MealFoods.MealId = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                SqlMapper.GridReader dataGrid = await connection.QueryMultipleAsync(sql, new { id });

                MealSummaryReadModel readModel = dataGrid.Read<MealSummaryReadModel, Time, MacronutrientTable, MealSummaryReadModel>((meal, time, macros) =>
                {
                    meal.TimeOfDay = time;
                    meal.MealMacronutrients = macros;
                    return meal;
                },
                splitOn: "Hour,Kcal")
                .FirstOrDefault();

                if (readModel != null)
                {
                    List<MealFood> foods = dataGrid.Read<MealFood, FoodUnit, MacronutrientTable, MealFood>((mealFood, foodUnit, macros) =>
                    {
                        return new MealFood(mealFood.Id, mealFood.Name, mealFood.Description, macros, foodUnit, mealFood.Quantity);
                    },
                    splitOn: "UnitType,Kcal")
                    .ToList();

                    readModel.Foods = foods;
                    readModel.FoodsCount = foods?.Count ?? 0;
                }

                return readModel;
            }
        }
    }
}