using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.Data.EntityTypeConfiguration.ValueObjects
{
    public class EmailAddressValueObjectConfiguration
    {
        public void Configure<T>(ReferenceOwnershipBuilder<T, EmailAddress> referenceOwnership) where T : class
        {
            Action<ReferenceOwnershipBuilder<T, EmailAddress>> setupAction = new Action<ReferenceOwnershipBuilder<T, EmailAddress>>(cfg =>
            {
                cfg.Property(emailAddress => emailAddress.Email).IsRequired().HasMaxLength(250);
            });

            setupAction(referenceOwnership);
        }
    }
}
