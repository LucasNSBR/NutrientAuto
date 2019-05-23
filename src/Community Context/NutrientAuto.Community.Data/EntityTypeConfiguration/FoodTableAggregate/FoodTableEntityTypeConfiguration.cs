using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.FoodTableAggregate
{
    public class FoodTableEntityTypeConfiguration : IEntityTypeConfiguration<FoodTable>
    {
        public void Configure(EntityTypeBuilder<FoodTable> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasDiscriminator<FoodTableType>(nameof(FoodTableType))
                .HasValue<FoodTable>(FoodTableType.Default)
                .HasValue<CustomFoodTable>(FoodTableType.Custom);

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
