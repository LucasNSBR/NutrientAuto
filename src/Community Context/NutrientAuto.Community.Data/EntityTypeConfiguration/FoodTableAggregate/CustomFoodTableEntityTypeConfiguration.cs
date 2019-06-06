using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.FoodTableAggregate
{
    public class CustomFoodTableEntityTypeConfiguration : IEntityTypeConfiguration<CustomFoodTable>
    {
        public void Configure(EntityTypeBuilder<CustomFoodTable> builder)
        {
            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(cft => cft.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
