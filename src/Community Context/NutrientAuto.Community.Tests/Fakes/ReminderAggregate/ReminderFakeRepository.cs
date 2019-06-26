using NutrientAuto.Community.Domain.Aggregates.ReminderAggregate;
using NutrientAuto.Community.Domain.Repositories.ReminderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Tests.Fakes.ReminderAggregate
{
    public class ReminderFakeRepository : IReminderRepository
    {
        public readonly List<Reminder> _reminders;

        public ReminderFakeRepository(List<Reminder> reminders = null)
        {
            _reminders = reminders ?? new List<Reminder>();
        }

        public Task<List<Reminder>> GetAllByProfileIdAsync(Guid profileId)
        {
            return Task.FromResult(_reminders.Where(reminder => reminder.ProfileId == profileId).ToList());
        }

        public Task<Reminder> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_reminders.FirstOrDefault(g => g.Id == id));
        }

        public Task RegisterAsync(Reminder entity)
        {
            _reminders.Add(entity);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Reminder entity)
        {
            _reminders.Remove(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Reminder entity)
        {
            int index = _reminders.FindIndex(g => g.Id == entity.Id);
            _reminders[index] = entity;

            return Task.CompletedTask;
        }
    }
}
