using MediatR;
using Microsoft.AspNetCore.Http;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.PostAggregate.BaseCommand
{
    public abstract class BasePostCommand : Command, IRequest<CommandResult>
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile AttachedImage { get; set; }
    }
}
