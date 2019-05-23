using FluentValidation;
using NutrientAuto.Community.Domain.Commands.ProfileAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.SeedWork;
using System;

namespace NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate.BaseCommandValidator
{
    public class BaseProfileCommandValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : BaseProfileCommand
    {
        public void ValidateProfileId()
        {
            RuleFor(command => command.ProfileId)
                .NotEmpty();
        }

        public void ValidateAvatarImage()
        {
            RuleFor(command => command.AvatarImage)
                .SetValidator(new FormFileValidator());
        }

        public void ValidateGenre()
        {
            RuleFor(command => command.Genre)
                .IsInEnum();
        }

        public void ValidateName()
        {
            RuleFor(command => command.Name)
                .Length(10, 150);
        }

        public void ValidateUsername()
        {
            RuleFor(command => command.Username)
                .Length(3, 30);
        }

        public void ValidateEmailAddress()
        {
            RuleFor(command => command.EmailAddress)
                .SetValidator(new EmailAddressDtoValidator());
        }

        public void ValidateBirthDate()
        {
            RuleFor(command => command.BirthDate)
                .ExclusiveBetween(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-6));
        }

        public void ValidateBio()
        {
            RuleFor(command => command.Bio)
                .MaximumLength(500);
        }
    }
}
