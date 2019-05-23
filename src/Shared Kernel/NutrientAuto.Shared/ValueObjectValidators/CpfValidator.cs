using FluentValidation;
using NutrientAuto.Shared.ValueObjects;

namespace NutrientAuto.Shared.ValueObjectValidators
{
    public class CpfValidator : AbstractValidator<Cpf>
    {
        public CpfValidator()
        {
            RuleFor(cpf => cpf.Number)
                .Matches("^[0-9]*$")
                .Length(11);
        }
    }
}
