using FluentValidation.Results;
using System;

namespace NutrientAuto.Shared.Commands
{
    public abstract class Command : ICommand
    {
        public Guid CommandId { get; }
        public DateTime DateCreated { get; }
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool Validate();

        protected Command()
        {
            CommandId = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }
    }
}
