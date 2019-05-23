using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Identity.Domain.Commands.UserAggregate.BaseCommand
{
    public abstract class RegisterUserBaseCommand : Command
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public Genre Genre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
