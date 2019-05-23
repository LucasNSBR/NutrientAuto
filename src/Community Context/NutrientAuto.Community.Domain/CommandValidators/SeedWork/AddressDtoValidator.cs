using FluentValidation;
using NutrientAuto.Community.Domain.Commands.SeedWork;

namespace NutrientAuto.Community.Domain.CommandValidators.SeedWork
{
    public class AddressDtoValidator : AbstractValidator<AddressDto>
    {
        public AddressDtoValidator()
        {
            RuleFor(address => address.Number)
                .ExclusiveBetween(1, 999999);

            RuleFor(address => address.Street)
                .NotEmpty()
                .MaximumLength(250);

            RuleFor(address => address.Neighborhood)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(address => address.Complementation)
                .MaximumLength(250);

            RuleFor(address => address.City)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(address => address.State)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(address => address.Country)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(address => address.Cep)
                .Length(8);
        }
    }
}
