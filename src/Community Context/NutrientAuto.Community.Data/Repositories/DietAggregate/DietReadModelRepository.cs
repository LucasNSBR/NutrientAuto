using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.ReadModels.DietAggregate;
using NutrientAuto.Community.Domain.Repositories.DietAggregate;
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
            string sql = $@"SELECT Diets.Id, Diets.ProfileId, Diets.Name, Diets.StartDate, Diets.DietTotalKcal AS Kcal, Diets.DietTotalKj AS Kj, Diets.DietTotalProtein AS Protein, Diets.DietTotalCarbohydrate AS Carbohydrate, Diets.DietTotalFat AS Fat
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
            string sql = $@"SELECT Diets.Id, Diets.ProfileId, Diets.Name, Diets.StartDate, Diets.DietTotalKcal AS Kcal, Diets.DietTotalKj AS Kj, Diets.DietTotalProtein AS Protein, Diets.DietTotalCarbohydrate AS Carbohydrate, Diets.DietTotalFat AS Fat
                         FROM Diets
                         WHERE Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                     .QueryAsync<DietSummaryReadModel, MacronutrientTable, DietSummaryReadModel>(sql,
                     (diet, macronutrientTable) =>
                     {
                         diet.TotalMacronutrients = macronutrientTable;
                         return diet;
                     },
                     new { id },
                     splitOn: "Kcal"))
                     .FirstOrDefault();
            }
        }
    }
}
