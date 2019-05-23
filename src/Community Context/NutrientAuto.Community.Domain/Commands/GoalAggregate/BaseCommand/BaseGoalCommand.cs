using MediatR;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.GoalAggregate.BaseCommand
{
    public abstract class BaseGoalCommand : Command, IRequest<CommandResult>
    {
        public Guid GoalId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public bool WritePost { get; set; }
    }
}
