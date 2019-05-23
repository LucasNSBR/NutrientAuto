using NutrientAuto.Community.Domain.Commands.MealAggregate.BaseCommand;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Community.Domain.CommandValidators.MealAggregate;

namespace NutrientAuto.Community.Domain.Commands.MealAggregate
{
    public class UpdateMealCommand : BaseMealCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeDto TimeOfDay { get; set; }

        public override bool Validate()
        {
            ValidationResult = new UpdateMealCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
