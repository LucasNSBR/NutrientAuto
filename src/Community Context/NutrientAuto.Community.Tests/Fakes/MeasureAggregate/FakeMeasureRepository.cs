using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Tests.Fakes.MeasureAggregate
{
    public class FakeMeasureRepository : IMeasureRepository
    {
        public readonly List<Measure> _measures;

        public FakeMeasureRepository(List<Measure> measures = null)
        {
            _measures = measures ?? new List<Measure>();
        }

        public Task<List<Measure>> GetAllByProfileIdAsync(Guid profileId)
        {
            return Task.FromResult(_measures.Where(goal => goal.ProfileId == profileId).ToList());
        }

        public Task<Measure> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_measures.FirstOrDefault(g => g.Id == id));
        }

        public Task RegisterAsync(Measure entity)
        {
            _measures.Add(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Measure entity)
        {
            _measures.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Measure entity)
        {
            int index = _measures.FindIndex(g => g.Id == entity.Id);
            _measures[index] = entity;

            return Task.CompletedTask;
        }
    }
}
