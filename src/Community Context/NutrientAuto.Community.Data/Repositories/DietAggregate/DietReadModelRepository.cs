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

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
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
            string sql = $@"SELECT Diets.Id, Diets.ProfileId, Diets.Name, Diets.StartDate, 
                         Diets.DietTotalKcal AS Kcal, Diets.DietTotalKj AS Kj, Diets.DietTotalProtein AS Protein, Diets.DietTotalCarbohydrate AS Carbohydrate, Diets.DietTotalFat AS Fat
                         FROM Diets
                         WHERE Id = @id;
                         
                         SELECT Meals.Id, Meals.Name, 
                         Meals.MealHour AS Hour, Meals.MealMinute AS Minute, Meals.MealSecond AS Second 
                         FROM Meals
                         WHERE Meals.DietId = @id";


            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                SqlMapper.GridReader dataGrid = await connection.QueryMultipleAsync(sql, new { id });

                DietSummaryReadModel readModel = dataGrid.Read<DietSummaryReadModel, MacronutrientTable, DietSummaryReadModel>((diet, macros) =>
                {
                    diet.TotalMacronutrients = macros;
                    return diet;
                },
                splitOn: "Kcal")
                .FirstOrDefault();

                if (readModel != null)
                {
                    List<DietMealReadModel> meals = dataGrid.Read<DietMealReadModel, Time, DietMealReadModel>((dietMeal, time) =>
                    {
                        return new DietMealReadModel
                        {
                            Id = dietMeal.Id,
                            Name = dietMeal.Name,
                            Time = time
                        };
                    },
                    splitOn: "Hour")
                    .ToList();

                    readModel.Meals = meals;
                    readModel.MealCount = meals?.Count ?? 0;
                }

                return readModel;
            }
        }
    }
}
