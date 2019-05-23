using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Shared.ValueObjects;
using System;

namespace NutrientAuto.Shared.Data.EntityTypeConfiguration.ValueObjects
{
    public class ImageValueObjectConfiguration
    {
        public void Configure<T>(ReferenceOwnershipBuilder<T, Image> referenceOwnership) where T : class
        {
            Action<ReferenceOwnershipBuilder<T, Image>> setupAction = new Action<ReferenceOwnershipBuilder<T, Image>>(cfg =>
            {
                cfg.Property(image => image.Name).IsRequired().HasMaxLength(100);
                cfg.Property(image => image.UrlPath).IsRequired().HasMaxLength(250);
            });

            setupAction(referenceOwnership);
        }
    }
}
