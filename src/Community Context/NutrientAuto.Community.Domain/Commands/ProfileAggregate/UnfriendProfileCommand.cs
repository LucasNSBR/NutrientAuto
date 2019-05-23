using NutrientAuto.Community.Domain.Commands.ProfileAggregate.BaseCommand;
using NutrientAuto.Community.Domain.CommandValidators.ProfileAggregate;
using System;

namespace NutrientAuto.Community.Domain.Commands.ProfileAggregate
{
    public class UnfriendProfileCommand : BaseProfileCommand
    {
        public Guid FriendProfileId { get; set; }

        public override bool Validate()
        {
            ValidationResult = new UnfriendProfileCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
