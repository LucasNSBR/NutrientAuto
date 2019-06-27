using NutrientAuto.Shared.Entities;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureStatisticsAggregate
{
    public class StatisticsWrapper : Entity<StatisticsWrapper>, IAggregateRoot
    {
        public Guid ProfileId { get; private set; }

        private readonly List<BasicStatisticEntry> _basicMeasures;
        public IReadOnlyList<BasicStatisticEntry> BasicMeasures => _basicMeasures;

        private readonly List<CategoryStatistics> _categoryStatistics;
        public IReadOnlyList<CategoryStatistics> CategoryStatistics => _categoryStatistics;

        public StatisticsWrapper(Guid profileId, List<BasicStatisticEntry> basicMeasures, List<CategoryStatistics> categoryStatistics)
        {
            ProfileId = profileId;
            _basicMeasures = basicMeasures ?? new List<BasicStatisticEntry>(); 
            _categoryStatistics = categoryStatistics ?? new List<CategoryStatistics>();
        }
    }
}
