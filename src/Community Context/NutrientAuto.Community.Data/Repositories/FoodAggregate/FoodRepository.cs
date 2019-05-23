using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Repositories.FoodAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.FoodAggregate
{
    public class FoodRepository : IFoodRepository
    {
        private readonly CommunityDbContext _dbContext;

        public FoodRepository(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Food>> GetAllDefaultsAsync()
        {
            return _dbContext
                .Foods
                .Where(f => f.FoodType == FoodType.Default)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<Food>> GetAllByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .Foods
                .Where(f => f.FoodType == FoodType.Custom && (f as CustomFood).ProfileId == profileId || f.FoodType == FoodType.Default)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<CustomFood>> GetAllCustomsByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .Foods
                .OfType<CustomFood>()
                .Where(f => f.ProfileId == profileId)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<Food>> GetAllByFoodTableIdAsync(Guid foodTableId, Guid profileId)
        {
            return _dbContext
                .Foods
                .Where(f => f.FoodTableId == foodTableId && (f.FoodType == FoodType.Custom && (f as CustomFood).ProfileId == profileId || f.FoodType == FoodType.Default))
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Food> GetDefaultByIdAsync(Guid id)
        {
            return _dbContext
                .Foods
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id && f.FoodType == FoodType.Default);
        }

        public Task<Food> GetByIdAsync(Guid id)
        {
            return _dbContext
                .Foods
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public Task<Food> GetByIdAndProfileIdAsync(Guid id, Guid profileId)
        {
            return _dbContext
                .Foods
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id && (f.FoodType == FoodType.Custom && (f as CustomFood).ProfileId == profileId || f.FoodType == FoodType.Default));
        }

        public Task<CustomFood> GetCustomByIdAsync(Guid id, Guid profileId)
        {
            return _dbContext
                .Foods
                .OfType<CustomFood>()
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id && f.ProfileId == profileId);
        }

        public Task RegisterAsync<TFood>(TFood food)
            where TFood : Food
        {
            return _dbContext
                .AddAsync(food);
        }

        public Task UpdateAsync<TFood>(TFood food)
            where TFood : Food
        {
            return Task.FromResult(_dbContext
                .Update(food));
        }

        public Task RemoveAsync<TFood>(TFood food)
            where TFood : Food
        {
            return Task.FromResult(_dbContext
                .Remove(food));
        }
    }
}
