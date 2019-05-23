using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using System;

namespace NutrientAuto.Community.Domain.ReadModels.ProfileAggregate
{
    public class ProfileSettingsReadModel
    {
        public Guid Id { get; set; }

        public ProfileSettings Settings { get; set; }
    }
}
