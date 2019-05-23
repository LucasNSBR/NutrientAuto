using MediatR;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.FoodTableAggregate.BaseCommand
{
    public abstract class BaseFoodTableCommand : Command, IRequest<CommandResult>
    {
        public Guid FoodTableId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
