using MediatR;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Shared.Commands;
using System;

namespace NutrientAuto.Community.Domain.Commands.ReminderAggregate.BaseCommand
{
    public abstract class BaseReminderCommand : Command, IRequest<CommandResult>
    {
        public Guid ReminderId { get; set; }
        public bool Active { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public TimeDto TimeOfDay { get; set; }
    }
}
