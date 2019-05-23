using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.Data.EntityTypeConfiguration.ValueObjects
{
    public class TimeValueObjectConfiguration
    {
        public void Configure<T>(ReferenceOwnershipBuilder<T, Time> referenceOwnership) where T : class
        {
            Action<ReferenceOwnershipBuilder<T, Time>> setupAction = new Action<ReferenceOwnershipBuilder<T, Time>>(cfg =>
            {
                //No setup required for now
            });

            setupAction(referenceOwnership);
        }
    }
}
