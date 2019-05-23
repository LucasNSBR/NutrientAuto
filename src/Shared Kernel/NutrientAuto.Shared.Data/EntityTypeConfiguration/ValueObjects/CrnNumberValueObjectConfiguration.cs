using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.Data.EntityTypeConfiguration.ValueObjects
{
    public class CrnNumberValueObjectConfiguration 
    {
        public void Configure<T>(ReferenceOwnershipBuilder<T, CrnNumber> referenceOwnership) where T : class
        {
            Action<ReferenceOwnershipBuilder<T, CrnNumber>> setupAction = new Action<ReferenceOwnershipBuilder<T, CrnNumber>>(cfg =>
            {
                cfg.Property(crnNumber => crnNumber.Number).IsRequired().HasMaxLength(12);
                cfg.HasIndex(crnNumber => crnNumber.Number);
            });

            setupAction(referenceOwnership);
        }
    }
}
