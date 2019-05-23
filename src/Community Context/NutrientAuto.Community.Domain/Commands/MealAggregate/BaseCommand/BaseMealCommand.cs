using MediatR;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.MealAggregate.BaseCommand
{
    public abstract class BaseMealCommand : Command, IRequest<CommandResult>
    {
        public Guid MealId { get; set; }
    }
}
