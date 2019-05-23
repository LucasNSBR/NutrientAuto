using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.Data.EntityTypeConfiguration.ValueObjects
{
    public class PhoneNumberValueObjectConfiguration
    {
        public void Configure<T>(ReferenceOwnershipBuilder<T, PhoneNumber> referenceOwnership) where T : class
        {
            Action<ReferenceOwnershipBuilder<T, PhoneNumber>> setupAction = new Action<ReferenceOwnershipBuilder<T, PhoneNumber>>(cfg =>
            {
                cfg.Property(phoneNumber => phoneNumber.Number).IsRequired().HasMaxLength(11);
            });

            setupAction(referenceOwnership);
        }

        public void Configure<T>(CollectionOwnershipBuilder<T, PhoneNumber> collectionOwnership) where T : class
        {
            Action<CollectionOwnershipBuilder<T, PhoneNumber>> setupAction = new Action<CollectionOwnershipBuilder<T, PhoneNumber>>(cfg =>
            {
                cfg.Property(phoneNumber => phoneNumber.Number).IsRequired().HasMaxLength(11);
            });

            setupAction(collectionOwnership);
        }
    }
}
