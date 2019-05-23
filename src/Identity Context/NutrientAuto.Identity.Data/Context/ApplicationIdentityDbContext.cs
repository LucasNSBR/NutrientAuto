using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Identity.Data.EntityTypeConfiguration.RoleAggregate;
using NutrientAuto.Identity.Data.EntityTypeConfiguration.UserAggregate;
using NutrientAuto.Identity.Domain.Aggregates.RoleAggregate;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using NutrientAuto.Identity.Domain.Context;
using System;

namespace NutrientAuto.Identity.Data.Context
{
    public class ApplicationIdentityDbContext : IdentityDbContext<NutrientIdentityUser, NutrientIdentityRole, Guid>, IApplicationIdentityDbContext
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new NutrientIdentityUserEntityTypeConfiguration());
            builder.ApplyConfiguration(new NutrientIdentityRoleEntityTypeConfiguration());
        }
    }
}
