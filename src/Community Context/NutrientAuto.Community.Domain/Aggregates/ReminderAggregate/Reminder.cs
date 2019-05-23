using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.Aggregates.ReminderAggregate
{
    public class Reminder : Entity<Reminder>, IAggregateRoot, IProfileEntity
    {
        public Guid ProfileId { get; private set; }

        public bool Active { get; private set; }
        public string Title { get; private set; }
        public string Details { get; private set; }
        public Time TimeOfDay { get; private set; }

        protected Reminder()
        {
        }

        public Reminder(Guid profileId, bool active, string title, string details, Time timeOfDay)
        {
            ProfileId = profileId;
            Active = active;
            Title = title;
            Details = details;
            TimeOfDay = timeOfDay;
        }

        public void Update(bool active, string title, string details, Time timeOfDay)
        {
            Active = active;
            Title = title;
            Details = details;
            TimeOfDay = timeOfDay;
        }
    }
}
