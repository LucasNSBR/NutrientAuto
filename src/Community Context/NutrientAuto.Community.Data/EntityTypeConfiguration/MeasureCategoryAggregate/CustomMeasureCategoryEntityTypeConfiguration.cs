using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.MeasureCategoryAggregate
{
    public class CustomMeasureCategoryEntityTypeConfiguration : IEntityTypeConfiguration<CustomMeasureCategory>
    {
        public void Configure(EntityTypeBuilder<CustomMeasureCategory> builder)
        {
            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(cft => cft.ProfileId);
        }
    }
}
