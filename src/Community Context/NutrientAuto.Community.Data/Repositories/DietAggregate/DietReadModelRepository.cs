using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.ReadModels.DietAggregate;
using NutrientAuto.Community.Domain.Repositories.DietAggregate;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.DietAggregate
{
    public class DietReadModelRepository : BaseReadModelRepository, IDietReadModelRepository
    {
        public DietReadModelRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<DietListReadModel>> GetDietListAsync(Guid profileId, string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = $@"SELECT Diets.Id, Diets.ProfileId, Diets.Name, Diets.StartDate, 
                         Diets.DietTotalKcal AS Kcal, Diets.DietTotalKj AS Kj, Diets.DietTotalProtein AS Protein, Diets.DietTotalCarbohydrate AS Carbohydrate, Diets.DietTotalFat AS Fat
                         FROM Diets
                         WHERE Diets.Name LIKE '%{@nameFilter ?? string.Empty}%'
                         ORDER BY Diets.StartDate DESC
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                return await connection
                     .QueryAsync<DietListReadModel, MacronutrientTable, DietListReadModel>(sql,
                     (diet, macronutrientTable) =>
                     {
                         diet.TotalMacronutrients = macronutrientTable;
                         return diet;
                     },
                     new { profileId, nameFilter = nameFilter ?? string.Empty, pageNumber, pageSize },
                     splitOn: "Kcal");
            }
        }

        public async Task<DietSummaryReadModel> GetDietSummaryAsync(Guid id)
        {
            string sql = $@"SELECT Diets.Id, Diets.ProfileId, Diets.Name, Diets.Description, Diets.StartDate, 
                         Diets.DietTotalKcal AS Kcal, Diets.DietTotalKj AS Kj, Diets.DietTotalProtein AS Protein, Diets.DietTotalCarbohydrate AS Carbohydrate, Diets.DietTotalFat AS Fat,
                         Meals.Id, Meals.Name, 
                         Meals.MealHour AS Hour, Meals.MealMinute AS Minute, Meals.MealSecond AS Second 
                         FROM Diets
                         LEFT JOIN Meals ON Meals.DietId = Diets.Id
                         WHERE Diets.Id = @id";

            using (DbConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                Dictionary<Guid, DietSummaryReadModel> rows = new Dictionary<Guid, DietSummaryReadModel>();

                return (await connection
                    .QueryAsync<DietSummaryReadModel, MacronutrientTable, DietMealReadModel, Time, DietSummaryReadModel>(sql,
                    (diet, dietMacros, meal, mealTime) =>
                    {
                        DietSummaryReadModel summary;

                        if(!rows.TryGetValue(id, out summary))
                        {
                            summary = diet;
                            summary.Meals = new List<DietMealReadModel>();
                            rows.Add(id, summary);
                        }

                        if (meal != null)
                        {
                            meal.Time = mealTime;
                            summary.Meals.Add(meal);
                        }

                        summary.MealCount = summary.Meals?.Count ?? 0;
                        summary.TotalMacronutrients = dietMacros;
                        return summary;
                    },
                    new { id },
                    splitOn: "Kcal,Id,Hour"))
                    .FirstOrDefault();
            }
        }
    }
}
