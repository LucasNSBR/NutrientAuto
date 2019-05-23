using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Aggregates.MeasureStatisticsAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainServices.MeasureStatisticsAggregate
{
    public interface IMeasureStatisticsDomainService
    {
        Task<StatisticsWrapper> GetBasicEntriesAndByCategoriesAsync(List<MeasureCategory> measureCategories, DateTime startDate, DateTime endDate);
        Task<List<CategoryStatistics>> GetEntriesByCategoriesAsync(List<MeasureCategory> measureCategories, DateTime startDate, DateTime endDate);
        Task<CategoryStatistics> GetEntriesByCategoryAsync(MeasureCategory measureCategory, DateTime startDate, DateTime endDate);
    }
}
