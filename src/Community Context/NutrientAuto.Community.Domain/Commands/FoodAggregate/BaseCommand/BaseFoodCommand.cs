using MediatR;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.FoodAggregate.BaseCommand
{
    public abstract class BaseFoodCommand : Command, IRequest<CommandResult>
    {
        public Guid FoodId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid FoodTableId { get; set; }
        public MacronutrientTableDto Macronutrients { get; set; }
        public MicronutrientTableDto Micronutrients { get; set; }
        public UnitType UnitType { get; set; }
        public decimal DefaultGramsQuantityMultiplier { get; set; }
    }
}
