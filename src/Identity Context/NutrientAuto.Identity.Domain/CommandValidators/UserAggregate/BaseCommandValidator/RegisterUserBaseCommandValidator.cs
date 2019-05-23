using FluentValidation;
using NutrientAuto.Identity.Domain.Commands.UserAggregate.BaseCommand;
using System;

namespace NutrientAuto.Identity.Domain.CommandValidators.UserAggregate.BaseCommandValidator
{
    public abstract class RegisterUserBaseCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : RegisterUserBaseCommand
    {
        public void ValidateName()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .Length(10, 100);
        }

        public void ValidateUsername()
        {
            RuleFor(command => command.Username)
                .NotEmpty()
                .Length(3, 30);
        }

        public void ValidateEmail()
        {
            RuleFor(command => command.Email)
                .EmailAddress()
                .Length(10, 150);
        }

        public void ValidatePassword()
        {
            RuleFor(command => command.Password)
                .NotEmpty()
                .Length(8, 24);
        }

        public void ValidateBirthDate()
        {
            RuleFor(command => command.BirthDate)
                .ExclusiveBetween(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-6));
        }
    }
}
