using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Identity.Domain.Aggregates.RoleAggregate;

namespace NutrientAuto.Identity.Data.EntityTypeConfiguration.RoleAggregate
{
    public class NutrientIdentityRoleEntityTypeConfiguration : IEntityTypeConfiguration<NutrientIdentityRole>
    {
        public void Configure(EntityTypeBuilder<NutrientIdentityRole> builder)
        {
            //This will be modified later
        }
    }
}
