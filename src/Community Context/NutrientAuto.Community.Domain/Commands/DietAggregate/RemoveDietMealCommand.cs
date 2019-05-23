using MediatR;
using NutrientAuto.Community.Domain.CommandValidators.DietAggregate;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.DietAggregate
{
    public class RemoveDietMealCommand : Command, IRequest<CommandResult>
    {
        public Guid DietId { get; set; }
        public Guid DietMealId { get; set; }

        public override bool Validate()
        {
            ValidationResult = new RemoveDietMealCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
