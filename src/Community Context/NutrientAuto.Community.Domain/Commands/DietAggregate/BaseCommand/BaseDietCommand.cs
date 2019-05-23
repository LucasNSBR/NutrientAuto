using MediatR;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.DietAggregate.BaseCommand
{
    public abstract class BaseDietCommand : Command, IRequest<CommandResult>
    {
        public Guid DietId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool WritePost { get; set; }
    }
}
