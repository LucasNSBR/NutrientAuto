using MediatR;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Community.Domain.CommandValidators.DietAggregate;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.DietAggregate
{
    public class AddDietMealCommand : Command, IRequest<CommandResult>
    {
        public Guid DietId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeDto TimeOfDay { get; set; }

        public override bool Validate()
        {
            ValidationResult = new AddDietMealCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
