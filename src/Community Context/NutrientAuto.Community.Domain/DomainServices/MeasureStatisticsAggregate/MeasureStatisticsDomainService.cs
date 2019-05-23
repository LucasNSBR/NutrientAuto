using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Aggregates.MeasureStatisticsAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainServices.MeasureStatisticsAggregate
{
    public class MeasureStatisticsDomainService : IMeasureStatisticsDomainService
    {
        private readonly IMeasureRepository _measureRepository;
        private readonly Guid _currentProfileId;

        public MeasureStatisticsDomainService(IMeasureRepository measureRepository, IIdentityService identityService)
        {
            _measureRepository = measureRepository;
            _currentProfileId = identityService.GetUserId();
        }

        public async Task<StatisticsWrapper> GetBasicEntriesAndByCategoriesAsync(List<MeasureCategory> measureCategories, DateTime startDate, DateTime endDate)
        {
            List<Measure> filteredMeasures = await GetMeasuresAsync(startDate, endDate);
            List<CategoryStatistics> measureStatistics = new List<CategoryStatistics>();
            List<BasicStatisticEntry> basicMeasures = new List<BasicStatisticEntry>();

            foreach (MeasureCategory measureCategory in measureCategories)
            {
                List<StatisticEntry> entries = new List<StatisticEntry>();

                foreach (Measure measure in filteredMeasures)
                {
                    measure.MeasureLines
                        .Where(ml => ml.MeasureCategoryId == measureCategory.Id)
                        .ToList()
                        .ForEach(ml => entries.Add(new StatisticEntry(measure.MeasureDate, ml.Value)));

                    basicMeasures.Add(new BasicStatisticEntry(measure.MeasureDate, measure.BasicMeasure));
                }

                measureStatistics.Add(new CategoryStatistics(measureCategory.Id, measureCategory.Name, entries.OrderByDescending(se => se.DateMeasure).ToList()));
            }

            return new StatisticsWrapper(_currentProfileId, basicMeasures.OrderByDescending(bse => bse.DateMeasure).ToList(), measureStatistics);
        }

        public async Task<List<CategoryStatistics>> GetEntriesByCategoriesAsync(List<MeasureCategory> measureCategories, DateTime startDate, DateTime endDate)
        {
            List<Measure> filteredMeasures = await GetMeasuresAsync(startDate, endDate);
            List<CategoryStatistics> measureStatistics = new List<CategoryStatistics>();

            foreach (MeasureCategory measureCategory in measureCategories)
            {
                List<StatisticEntry> entries = new List<StatisticEntry>();

                foreach (Measure measure in filteredMeasures)
                {
                    measure.MeasureLines
                        .Where(ml => ml.MeasureCategoryId == measureCategory.Id)
                        .ToList()
                        .ForEach(ml => entries.Add(new StatisticEntry(measure.MeasureDate, ml.Value)));
                }

                measureStatistics.Add(new CategoryStatistics(measureCategory.Id, measureCategory.Name, entries.OrderByDescending(se => se.DateMeasure).ToList()));
            }

            return measureStatistics;
        }

        public async Task<CategoryStatistics> GetEntriesByCategoryAsync(MeasureCategory measureCategory, DateTime startDate, DateTime endDate)
        {
            List<Measure> filteredMeasures = await GetMeasuresAsync(startDate, endDate);
            List<StatisticEntry> entries = new List<StatisticEntry>();

            foreach (Measure measure in filteredMeasures)
            {
                measure.MeasureLines
                    .Where(ml => ml.MeasureCategoryId == measureCategory.Id)
                    .ToList()
                    .ForEach(ml => entries.Add(new StatisticEntry(measure.MeasureDate, ml.Value)));
            }

            return new CategoryStatistics(measureCategory.Id, measureCategory.Name, entries.OrderByDescending(se => se.DateMeasure).ToList());
        }

        private async Task<List<Measure>> GetMeasuresAsync(DateTime startDate, DateTime endDate)
        {
            List<Measure> measures = await _measureRepository.GetAllByProfileIdAsync(_currentProfileId);

            return measures
                .Where(m => m.MeasureDate > startDate && m.MeasureDate < endDate)
                .ToList();
        }
    }
}
