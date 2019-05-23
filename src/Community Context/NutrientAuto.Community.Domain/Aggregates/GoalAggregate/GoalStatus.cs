using NutrientAuto.Shared.ValueObjects;
using System;
using System.Text;

namespace NutrientAuto.Community.Domain.Aggregates.GoalAggregate
{
    public class GoalStatus : ValueObject<GoalStatus>
    {
        public bool IsCompleted { get; private set; }
        public DateTime? DateCompleted { get; private set; }
        public string AccomplishmentDetails { get; private set; }

        protected GoalStatus()
        {
        }

        private GoalStatus(bool isCompleted, DateTime? dateCompleted = null, string accomplishmentDetails = null)
        {
            IsCompleted = isCompleted;
            DateCompleted = dateCompleted;
            AccomplishmentDetails = accomplishmentDetails;
        }

        public static GoalStatus Uncompleted()
        {
            return new GoalStatus(false, null, null);
        }

        public static GoalStatus Completed(DateTime dateCompleted, string accomplishmentDetails)
        {
            return new GoalStatus(true, dateCompleted, accomplishmentDetails);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"Completado: {IsCompleted}")
                .AppendLine($"Data da completação: {DateCompleted?.ToShortDateString() ?? "Não registrado"}")
                .AppendLine($"Detalhes da completação: {AccomplishmentDetails ?? "Não registrado"}")
                .ToString();
        }
    }
}
