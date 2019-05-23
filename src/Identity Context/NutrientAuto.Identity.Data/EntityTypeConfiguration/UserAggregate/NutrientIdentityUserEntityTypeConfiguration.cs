using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;

namespace NutrientAuto.Identity.Data.EntityTypeConfiguration.UserAggregate
{
    public class NutrientIdentityUserEntityTypeConfiguration : IEntityTypeConfiguration<NutrientIdentityUser>
    {
        public void Configure(EntityTypeBuilder<NutrientIdentityUser> builder)
        {
            //This will be modified later
        }
    }
}
