using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.FoodAggregate
{
    public class CustomFoodEntityTypeConfiguration : IEntityTypeConfiguration<CustomFood>
    {
        public void Configure(EntityTypeBuilder<CustomFood> builder)
        {
            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(cf => cf.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
