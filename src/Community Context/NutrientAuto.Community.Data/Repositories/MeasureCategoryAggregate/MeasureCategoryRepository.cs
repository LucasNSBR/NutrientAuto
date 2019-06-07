using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.MeasureCategoryAggregate
{
    public class MeasureCategoryRepository : IMeasureCategoryRepository
    {
        private readonly CommunityDbContext _dbContext;

        public MeasureCategoryRepository(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<MeasureCategory>> GetAllDefaultsAsync()
        {
            return _dbContext
                .MeasureCategories
                .Where(mc => mc.MeasureCategoryType == MeasureCategoryType.Default)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<MeasureCategory>> GetAllByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .MeasureCategories
                .Where(mc => mc.MeasureCategoryType == MeasureCategoryType.Custom && (mc as CustomMeasureCategory).ProfileId == profileId || mc.MeasureCategoryType == MeasureCategoryType.Default)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<MeasureCategory>> GetAllFavoritesByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .MeasureCategories
                .Where(mc => mc.IsFavorite && (mc.MeasureCategoryType == MeasureCategoryType.Custom && (mc as CustomMeasureCategory).ProfileId == profileId || mc.MeasureCategoryType == MeasureCategoryType.Default))
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<CustomMeasureCategory>> GetAllCustomsByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .MeasureCategories
                .OfType<CustomMeasureCategory>()
                .AsNoTracking()
                .Where(mc => mc.ProfileId == profileId)
                .ToListAsync();
        }

        public Task<MeasureCategory> GetDefaultByIdAsync(Guid id)
        {
            return _dbContext
                .MeasureCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(mc => mc.Id == id && mc.MeasureCategoryType == MeasureCategoryType.Default);
        }

        public Task<MeasureCategory> GetByIdAsync(Guid id)
        {
            return _dbContext
                .MeasureCategories
                .FirstOrDefaultAsync(mc => mc.Id == id);
        }

        public Task<MeasureCategory> GetByIdAndProfileIdAsync(Guid id, Guid profileId)
        {
            return _dbContext
                .MeasureCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(mc => mc.Id == id && (mc.MeasureCategoryType == MeasureCategoryType.Custom && (mc as CustomMeasureCategory).ProfileId == profileId || mc.MeasureCategoryType == MeasureCategoryType.Default));
        }

        public Task<CustomMeasureCategory> GetCustomByIdAsync(Guid id, Guid profileId)
        {
            return _dbContext
                .MeasureCategories
                .OfType<CustomMeasureCategory>()
                .FirstOrDefaultAsync(ft => ft.Id == id && ft.ProfileId == profileId);
        }

        public Task RegisterAsync<TMeasureCategory>(TMeasureCategory measureCategory)
            where TMeasureCategory : MeasureCategory
        {
            return _dbContext
                .AddAsync(measureCategory);
        }

        public Task UpdateAsync<TMeasureCategory>(TMeasureCategory measureCategory)
            where TMeasureCategory : MeasureCategory
        {
            return Task.FromResult(_dbContext
                .Update(measureCategory));
        }

        public Task RemoveAsync<TMeasureCategory>(TMeasureCategory measureCategory)
            where TMeasureCategory : MeasureCategory
        {
            return Task.FromResult(_dbContext
                .Remove(measureCategory));
        }
    }
}
