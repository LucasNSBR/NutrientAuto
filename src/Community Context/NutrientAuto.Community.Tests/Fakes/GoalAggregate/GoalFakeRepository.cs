using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using NutrientAuto.Community.Domain.Repositories.GoalAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Tests.Fakes.GoalAggregate
{
    public class GoalFakeRepository : IGoalRepository
    {
        public readonly List<Goal> _goals;

        public GoalFakeRepository(List<Goal> goals = null)
        {
            _goals = goals ?? new List<Goal>();
        }

        public Task<List<Goal>> GetAllByProfileIdAsync(Guid profileId)
        {
            return Task.FromResult(_goals.Where(goal => goal.ProfileId == profileId).ToList());
        }

        public Task<Goal> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_goals.FirstOrDefault(g => g.Id == id));
        }

        public Task RegisterAsync(Goal entity)
        {
            _goals.Add(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Goal entity)
        {
            _goals.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Goal entity)
        {
            int index = _goals.FindIndex(g => g.Id == entity.Id);
            _goals[index] = entity;

            return Task.CompletedTask;
        }
    }
}
