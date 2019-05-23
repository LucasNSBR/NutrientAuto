using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.Data.EntityTypeConfiguration.ValueObjects
{
    public class CpfValueObjectConfiguration
    {
        public void Configure<T>(ReferenceOwnershipBuilder<T, Cpf> referenceOwnership) where T : class
        {
            Action<ReferenceOwnershipBuilder<T, Cpf>> setupAction = new Action<ReferenceOwnershipBuilder<T, Cpf>>(cfg =>
            {
                cfg.Property(cpf => cpf.Number).IsRequired().HasMaxLength(11);
            });

            setupAction(referenceOwnership);
        }
    }
}
