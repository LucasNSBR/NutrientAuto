﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Shared.Data.Extensions;
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
                    cfg.Property(mm => mm.Kcal).HasColumnName("MealTotalKcal").HasPrecision(18, 2);
                    cfg.Property(mm => mm.Kj).HasColumnName("MealTotalKj").HasPrecision(18, 2);
                    cfg.Property(mm => mm.Protein).HasColumnName("MealTotalProtein").HasPrecision(18, 2);
                    cfg.Property(mm => mm.Carbohydrate).HasColumnName("MealTotalCarbohydrate").HasPrecision(18, 2);
                    cfg.Property(mm => mm.Fat).HasColumnName("MealTotalFat").HasPrecision(18, 2);
                });

            builder
                .Ignore(m => m.MealFoodCount);

            builder
                .OwnsMany(m => m.MealFoods, cfg =>
                {
                    cfg.Property<Guid>("Id");
                    cfg.HasKey("Id");

                    cfg.HasOne(mf => mf.Food)
                    .WithMany()
                    .HasForeignKey(mf => mf.FoodId)
                    .OnDelete(DeleteBehavior.Restrict);

                    cfg.Property(mf => mf.Quantity).HasPrecision(18, 2);

                    cfg.OwnsOne(mf => mf.Macronutrients, macroCfg =>
                    {
                        macroCfg.Property(fm => fm.Kcal).HasColumnName("MealFoodKcal").HasPrecision(18, 2);
                        macroCfg.Property(fm => fm.Kj).HasColumnName("MealFoodKj").HasPrecision(18, 2);
                        macroCfg.Property(fm => fm.Protein).HasColumnName("MealFoodProtein").HasPrecision(18, 2);
                        macroCfg.Property(fm => fm.Carbohydrate).HasColumnName("MealFoodCarbohydrate").HasPrecision(18, 2);
                        macroCfg.Property(fm => fm.Fat).HasColumnName("MealFoodFat").HasPrecision(18, 2);
                    });

                    cfg.OnDelete(DeleteBehavior.Restrict);
                    cfg.ToTable("MealFoods");
                });
        }
    }
}
