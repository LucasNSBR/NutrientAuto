﻿using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.ReadModels.MealAggregate;
using NutrientAuto.Community.Domain.Repositories.MealAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
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
            string sql = $@"SELECT Meals.Id, Meals.ProfileId, Meals.DietId, Meals.Name, 
                         Meals.MealHour AS Hour, Meals.MealMinute AS Minute, Meals.MealSecond AS Second, 
                         Meals.MealTotalKcal AS Kcal, Meals.MealTotalKj AS Kj, Meals.MealTotalProtein AS Protein, Meals.MealTotalCarbohydrate AS Carbohydrate, Meals.MealTotalFat AS Fat,
                         MealFoods.Id, MealFoods.FoodId, MealFoods.Quantity, Foods.Name AS Name, Foods.Description AS Description,
                         Foods.FoodUnitType AS UnitType, Foods.FoodDefaultGramsQuantityMultiplier AS DefaultGramsQuantityMultiplier,
                         MealFoods.MealFoodKcal AS Kcal, MealFoods.MealFoodKj AS Kj, MealFoods.MealFoodProtein AS Protein, MealFoods.MealFoodCarbohydrate AS Carbohydrate, MealFoods.MealFoodFat AS Fat
                         FROM Meals
                         LEFT JOIN MealFoods ON Meals.Id = MealFoods.MealId
                         LEFT JOIN Foods ON MealFoods.FoodId = Foods.Id
                         WHERE Meals.Id = @id";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                Dictionary<Guid, MealSummaryReadModel> rows = new Dictionary<Guid, MealSummaryReadModel>();

                return (await connection
                    .QueryAsync<MealSummaryReadModel, Time, MacronutrientTable, MealFoodReadModel, FoodUnit, MacronutrientTable, MealSummaryReadModel>(sql,
                    (meal, mealTime, mealMacros, mealFood, mealFoodUnit, mealFoodMacros) =>
                    {
                        MealSummaryReadModel summary;

                        if (!rows.TryGetValue(id, out summary))
                        {
                            summary = meal;
                            summary.Foods = new List<MealFoodReadModel>();
                            rows.Add(id, summary);
                        }

                        mealFood.FoodUnit = mealFoodUnit;
                        mealFood.Macronutrients = mealFoodMacros;
                        summary.Foods.Add(mealFood);

                        summary.FoodsCount = summary.Foods?.Count ?? 0;
                        summary.MealMacronutrients = mealMacros;
                        return summary;
                    },
                    new { id },
                    splitOn: "Hour,Kcal,Id,UnitType,Kcal"))
                    .FirstOrDefault();
            }
        }
    }
}