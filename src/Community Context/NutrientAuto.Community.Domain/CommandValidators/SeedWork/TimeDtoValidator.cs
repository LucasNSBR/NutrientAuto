using FluentValidation;
using NutrientAuto.Community.Domain.Commands.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.SeedWork
{
    public class TimeDtoValidator : AbstractValidator<TimeDto>
    {
        public TimeDtoValidator()
        {
            RuleFor(timeDto => timeDto.Hour)
                .InclusiveBetween(0, 23);

            RuleFor(timeDto => timeDto.Hour)
                .InclusiveBetween(0, 59);

            RuleFor(timeDto => timeDto.Hour)
                .InclusiveBetween(0, 59);
        }
    }
}
