using NutrientAuto.Shared.Entities;
using System;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.Aggregates.MeasureStatisticsAggregate
{
    public class StatisticsWrapper : Entity<StatisticsWrapper>, IAggregateRoot
    {
        public Guid ProfileId { get; private set; }

        private readonly List<BasicStatisticEntry> _basicMeasures = new List<BasicStatisticEntry>();
        public IReadOnlyList<BasicStatisticEntry> BasicMeasures => _basicMeasures;

        private readonly List<CategoryStatistics> _categoryStatistics = new List<CategoryStatistics>();
        public IReadOnlyList<CategoryStatistics> CategoryStatistics => _categoryStatistics;

        public StatisticsWrapper(Guid profileId, List<BasicStatisticEntry> basicMeasures, List<CategoryStatistics> categoryStatistics)
        {
            ProfileId = profileId;
            _basicMeasures = basicMeasures;
            _categoryStatistics = categoryStatistics;
        }
    }
}
