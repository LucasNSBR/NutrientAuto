using MediatR;
using Microsoft.AspNetCore.Http;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Community.Domain.Commands.ProfileAggregate.BaseCommand
{
    public abstract class BaseProfileCommand : Command, IRequest<CommandResult>
    {
        public Guid ProfileId { get; set; }
        public IFormFile AvatarImage { get; set; }
        public Genre Genre { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public DateTime BirthDate { get; set; }
        public string Bio { get; set; }

        public bool WritePost { get; set; }
    }
}
