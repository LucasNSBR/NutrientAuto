using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using NutrientAuto.Community.Domain.Repositories.FoodTableAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.FoodTableAggregate
{
    public class FoodTableRepository : IFoodTableRepository
    {
        private readonly CommunityDbContext _dbContext;

        public FoodTableRepository(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<FoodTable>> GetAllDefaultsAsync()
        {
            return _dbContext
                .FoodTables
                .Where(f => f.FoodTableType == FoodTableType.Default)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<FoodTable>> GetAllByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .FoodTables
                .Where(ft => ft.FoodTableType == FoodTableType.Custom && (ft as CustomFoodTable).ProfileId == profileId || ft.FoodTableType == FoodTableType.Default)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<CustomFoodTable>> GetAllCustomsByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .FoodTables
                .OfType<CustomFoodTable>()
                .AsNoTracking()
                .Where(ft => ft.ProfileId == profileId)
                .ToListAsync();
        }

        public Task<FoodTable> GetDefaultByIdAsync(Guid id)
        {
            return _dbContext
                .FoodTables
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id && f.FoodTableType == FoodTableType.Default);
        }

        public Task<FoodTable> GetByIdAsync(Guid id)
        {
            return _dbContext
                .FoodTables
                .FirstOrDefaultAsync(ft => ft.Id == id);
        }

        public Task<FoodTable> GetByIdAndProfileIdAsync(Guid id, Guid profileId)
        {
            return _dbContext
                .FoodTables
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id && (f.FoodTableType == FoodTableType.Custom && (f as CustomFoodTable).ProfileId == profileId || f.FoodTableType == FoodTableType.Default));
        }

        public Task<CustomFoodTable> GetCustomByIdAsync(Guid id, Guid profileId)
        {
            return _dbContext
                .FoodTables
                .OfType<CustomFoodTable>()
                .FirstOrDefaultAsync(ft => ft.Id == id && ft.ProfileId == profileId);
        }

        public Task RegisterAsync<TFoodTable>(TFoodTable foodTable)
            where TFoodTable : FoodTable
        {
            return _dbContext
                .AddAsync(foodTable);
        }

        public Task UpdateAsync<TFoodTable>(TFoodTable foodTable)
            where TFoodTable : FoodTable
        {
            return Task.FromResult(_dbContext
                .Update(foodTable));
        }

        public Task RemoveAsync<TFoodTable>(TFoodTable foodTable)
            where TFoodTable : FoodTable
        {
            return Task.FromResult(_dbContext
                .Remove(foodTable));
        }
    }
}
