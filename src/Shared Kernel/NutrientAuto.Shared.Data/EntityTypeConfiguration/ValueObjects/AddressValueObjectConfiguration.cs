using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.Data.EntityTypeConfiguration.ValueObjects
{
    public class AddressValueObjectConfiguration
    {
        public void Configure<T>(ReferenceOwnershipBuilder<T, Address> referenceOwnership) where T : class
        {
            Action<ReferenceOwnershipBuilder<T, Address>> setupAction = new Action<ReferenceOwnershipBuilder<T, Address>>(cfg =>
            {
                cfg.Property(address => address.Street).IsRequired().HasMaxLength(250);
                cfg.Property(address => address.Neighborhood).IsRequired().HasMaxLength(100);
                cfg.Property(address => address.Complementation).HasMaxLength(250);
                cfg.Property(address => address.City).IsRequired().HasMaxLength(150);
                cfg.Property(address => address.State).IsRequired().HasMaxLength(50);
                cfg.Property(address => address.Country).IsRequired().HasMaxLength(100);
                cfg.Property(address => address.Cep).IsRequired().HasMaxLength(8);
            });

            setupAction(referenceOwnership);
        }
    }
}
