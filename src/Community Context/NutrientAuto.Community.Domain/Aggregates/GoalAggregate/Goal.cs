using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using System;

namespace NutrientAuto.Community.Domain.Aggregates.GoalAggregate
{
    public class Goal : Entity<Goal>, IAggregateRoot, IProfileEntity
    {
        public Guid ProfileId { get; private set; }

        public GoalStatus Status { get; private set; }
        public string Title { get; private set; }
        public string Details { get; private set; }

        public DateTime DateCreated { get; private set; }

        protected Goal()
        {
        }

        public Goal(Guid profileId, string title, string details)
        {
            Status = GoalStatus.Uncompleted();
            ProfileId = profileId;
            Title = title;
            Details = details;
            DateCreated = DateTime.Now;
        }

        public void Update(string title, string details)
        {
            Title = title;
            Details = details;
        }

        public void SetUncompleted()
        {
            if (!Status.IsCompleted)
                AddNotification("Status incompleto", "Esse objetivo já está marcado como incompleto.");

            Status = GoalStatus.Uncompleted();
        }

        public void SetCompleted(DateTime dateCompleted, string accomplishmentDetails)
        {
            if (Status.IsCompleted)
                AddNotification("Status completo", "Esse objetivo já está marcado como completo.");
            if (dateCompleted < DateCreated)
                AddNotification("Datas inválidas", "A data em que você cumpriu esse objetivo não pode ser menor que a data de criação.");

            Status = GoalStatus.Completed(dateCompleted, accomplishmentDetails);
        }
    }
}

