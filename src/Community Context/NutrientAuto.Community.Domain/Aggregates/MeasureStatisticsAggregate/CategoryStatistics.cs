using NutrientAuto.Shared.Entities;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureStatisticsAggregate
{
    public class CategoryStatistics : Entity<CategoryStatistics>, IAggregateRoot
    {
        public Guid MeasureCategoryId { get; private set; }
        public string MeasureCategoryName { get; private set; }

        private readonly List<StatisticEntry> _entries;
        public IReadOnlyList<StatisticEntry> Entries => _entries;

        public CategoryStatistics(Guid measureCategoryId, string measureCategoryName, List<StatisticEntry> entries)
        {
            MeasureCategoryId = measureCategoryId;
            MeasureCategoryName = measureCategoryName;
            _entries = entries;
        }
    }
}
