using FluentValidation;
using NutrientAuto.Community.Domain.Commands.ProfileAggregate;
using NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate.BaseCommandValidator;

namespace NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate
{
    public class UnfriendProfileCommandValidator : BaseProfileCommandValidator<UnfriendProfileCommand>
    {
        public UnfriendProfileCommandValidator()
        {
            RuleFor(command => command.FriendProfileId)
                .NotEmpty();
        }
    }
}
