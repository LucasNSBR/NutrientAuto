using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate
{
    public interface IMeasureCategoryRepository
    {
        Task<List<MeasureCategory>> GetAllDefaultsAsync();
        Task<List<MeasureCategory>> GetAllByProfileIdAsync(Guid profileId);
        Task<List<MeasureCategory>> GetAllFavoritesByProfileIdAsync(Guid profileId);
        Task<List<CustomMeasureCategory>> GetAllCustomsByProfileIdAsync(Guid profileId);
        Task<MeasureCategory> GetDefaultByIdAsync(Guid id);
        Task<MeasureCategory> GetByIdAsync(Guid id);
        Task<MeasureCategory> GetByIdAndProfileIdAsync(Guid id, Guid profileId);
        Task<CustomMeasureCategory> GetCustomByIdAsync(Guid id, Guid profileId);
        Task RegisterAsync<TMeasureCategory>(TMeasureCategory measureCategory) where TMeasureCategory : MeasureCategory;
        Task UpdateAsync<TMeasureCategory>(TMeasureCategory measureCategory) where TMeasureCategory : MeasureCategory;
        Task RemoveAsync<TMeasureCategory>(TMeasureCategory measureCategory) where TMeasureCategory : MeasureCategory;
    }
}
