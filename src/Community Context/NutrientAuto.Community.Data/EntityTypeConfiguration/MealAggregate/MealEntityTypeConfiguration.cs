using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using System;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.MealAggregate
{
    public class MealEntityTypeConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .OwnsOne(m => m.TimeOfDay, cfg =>
                {
                    cfg.Property(t => t.Hour).HasColumnName("MealHour");
                    cfg.Property(t => t.Minute).HasColumnName("MealMinute");
                    cfg.Property(t => t.Second).HasColumnName("MealSecond");
                });

            builder
                .OwnsOne(m => m.MealMacronutrients, cfg =>
                {
                    cfg.Property(mm => mm.Kcal).HasColumnName("MealTotalKcal");
                    cfg.Property(mm => mm.Kj).HasColumnName("MealTotalKj");
                    cfg.Property(mm => mm.Protein).HasColumnName("MealTotalProtein");
                    cfg.Property(mm => mm.Carbohydrate).HasColumnName("MealTotalCarbohydrate");
                    cfg.Property(mm => mm.Fat).HasColumnName("MealTotalFat");
                });

            builder
                .Ignore(m => m.MealFoodCount);

            builder
                .OwnsMany(m => m.MealFoods, cfg =>
                {
                    cfg.Property<Guid>("Id");
                    cfg.HasKey("Id");
                    cfg.Property(mf => mf.Name).IsRequired().HasMaxLength(100);
                    cfg.Property(mf => mf.Description).IsRequired().HasMaxLength(250);

                    cfg.OwnsOne(mf => mf.Macronutrients, macroCfg =>
                    {
                        macroCfg.Property(fm => fm.Kcal).HasColumnName("MealFoodKcal");
                        macroCfg.Property(fm => fm.Kj).HasColumnName("MealFoodKj");
                        macroCfg.Property(fm => fm.Protein).HasColumnName("MealFoodProtein");
                        macroCfg.Property(fm => fm.Carbohydrate).HasColumnName("MealFoodCarbohydrate");
                        macroCfg.Property(fm => fm.Fat).HasColumnName("MealFoodFat");
                    });

                    cfg.OwnsOne(mf => mf.FoodUnit, unitCfg =>
                    {
                        unitCfg.Property(fu => fu.UnitType).HasColumnName("MealFoodUnitType");
                        unitCfg.Property(fu => fu.DefaultGramsQuantityMultiplier).HasColumnName("MealFoodDefaultGramsQuantityMultiplier");
                    });

                    cfg.ToTable("MealFoods");
                });
        }
    }
}
