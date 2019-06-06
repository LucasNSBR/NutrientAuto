using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.DietAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Shared.Data.Extensions;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.DietAggregate
{
    public class DietEntityTypeConfiguration : IEntityTypeConfiguration<Diet>
    {
        public void Configure(EntityTypeBuilder<Diet> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(d => d.ProfileId);

            builder
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(d => d.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(d => d.StartDate)
                .IsRequired();
            
            builder
                .OwnsOne(d => d.TotalMacronutrients, macronutrientTableCfg =>
                {
                    macronutrientTableCfg.Property(mt => mt.Kcal).HasColumnName("DietTotalKcal").HasPrecision(18, 2);
                    macronutrientTableCfg.Property(mt => mt.Kj).HasColumnName("DietTotalKj").HasPrecision(18, 2);
                    macronutrientTableCfg.Property(mt => mt.Carbohydrate).HasColumnName("DietTotalCarbohydrate").HasPrecision(18, 2);
                    macronutrientTableCfg.Property(mt => mt.Protein).HasColumnName("DietTotalProtein").HasPrecision(18, 2);
                    macronutrientTableCfg.Property(mt => mt.Fat).HasColumnName("DietTotalFat").HasPrecision(18, 2);
                });

            builder
                .Ignore(d => d.MealCount);

            builder
               .HasMany(d => d.DietMeals)
               .WithOne()
               .HasForeignKey(m => m.DietId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
