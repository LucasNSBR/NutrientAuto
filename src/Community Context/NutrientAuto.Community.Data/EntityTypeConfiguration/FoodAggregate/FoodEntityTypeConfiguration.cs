using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.FoodAggregate
{
    public class FoodEntityTypeConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
               .HasDiscriminator<FoodType>(nameof(FoodType))
               .HasValue<Food>(FoodType.Default)
               .HasValue<CustomFood>(FoodType.Custom);

            builder
                .Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(f => f.Description)
                .HasMaxLength(250);

            builder
                .HasOne<FoodTable>()
                .WithMany()
                .HasForeignKey(f => f.FoodTableId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .OwnsOne(f => f.Macronutrients);

            builder
                .OwnsOne(f => f.Micronutrients);

            builder
                .OwnsOne(f => f.FoodUnit);
        }
    }
}
