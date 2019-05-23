using System;

namespace NutrientAuto.Community.Domain.Aggregates.SeedWork
{
    public interface IProfileEntity
    {
        Guid Id { get; }
        Guid ProfileId { get; }
    }
}
