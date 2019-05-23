using System;

namespace NutrientAuto.Shared.Entities
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
