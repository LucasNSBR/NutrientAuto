using System;

namespace NutrientAuto.Shared.Commands
{
    public interface ICommand
    {
        Guid CommandId { get; }
        DateTime DateCreated { get; }
    }
}
