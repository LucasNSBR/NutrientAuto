using MediatR;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate.BaseCommand
{
    public abstract class BaseMeasureCategoryCommand : Command, IRequest<CommandResult>
    {
        public Guid MeasureCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }
    }
}
