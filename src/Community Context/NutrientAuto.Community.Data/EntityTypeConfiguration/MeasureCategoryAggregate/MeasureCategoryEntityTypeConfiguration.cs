using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.MeasureCategoryAggregate
{
    public class MeasureCategoryEntityTypeConfiguration : IEntityTypeConfiguration<MeasureCategory>
    {
        public void Configure(EntityTypeBuilder<MeasureCategory> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasDiscriminator<MeasureCategoryType>(nameof(MeasureCategoryType))
                .HasValue<MeasureCategory>(MeasureCategoryType.Default)
                .HasValue<CustomMeasureCategory>(MeasureCategoryType.Custom);

            builder
                .Property(ft => ft.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(ft => ft.Description)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
