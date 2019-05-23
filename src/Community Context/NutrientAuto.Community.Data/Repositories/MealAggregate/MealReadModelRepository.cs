using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.ReadModels.MealAggregate;
using NutrientAuto.Community.Domain.Repositories.MealAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
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
            string sql = @"SELECT Meal.Id, Meal.ProfileId, Meal.DietId, Meal.Name, 
                         Meal.MealHour AS MealHour, Meal.MealMinute AS MealMinute, Meal.MealSecond AS MealSecond
                         Meal.MealKcal AS MealKcal, Meal.MealKj AS MealKj, Mael.MealProtein MealProtein, Meal.MealCarbohydrate AS MealCarbohydrate, Meal.MealFat AS MealFat
                         FROM Meals 
                         WHERE Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                    .QueryAsync<MealSummaryReadModel, Time, MacronutrientTable, MealSummaryReadModel>(sql,
                    (meal, time, macronutrientTable) =>
                    {
                        meal.TimeOfDay = time;
                        meal.MealMacronutrients = macronutrientTable;
                        return meal;
                    },
                    new { id },
                    splitOn: "MealHour,MealKcal"))
                    .FirstOrDefault();
            }
        }
    }
}
