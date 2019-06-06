using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using NutrientAuto.Shared.Data.Extensions;

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
                .OwnsOne(f => f.Macronutrients, macroCfg =>
                {
                    macroCfg.Property(fm => fm.Kcal).HasColumnName("FoodKcal").HasPrecision(18, 2);
                    macroCfg.Property(fm => fm.Kj).HasColumnName("FoodKj").HasPrecision(18, 2);
                    macroCfg.Property(fm => fm.Protein).HasColumnName("FoodProtein").HasPrecision(18, 2);
                    macroCfg.Property(fm => fm.Carbohydrate).HasColumnName("FoodCarbohydrate").HasPrecision(18, 2);
                    macroCfg.Property(fm => fm.Fat).HasColumnName("FoodFat").HasPrecision(18, 2);
                });

            builder
                .OwnsOne(f => f.Micronutrients, microCfg =>
                {
                    microCfg.Property(fm => fm.Calcium).HasColumnName("MicronutrientsCalcium").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Chromium).HasColumnName("MicronutrientsChromium").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Copper).HasColumnName("MicronutrientsCopper").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Magnesium).HasColumnName("MicronutrientsMagnesium").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Manganese).HasColumnName("MicronutrientsManganese").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Phosphorus).HasColumnName("MicronutrientsPhosphorus").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Potassium).HasColumnName("MicronutrientsPotassium").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Sodium).HasColumnName("MicronutrientsSodium").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Selenium).HasColumnName("MicronutrientsSelenium").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.Zinc).HasColumnName("MicronutrientsZinc").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.VitaminB1).HasColumnName("MicronutrientsVitaminB1").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.VitaminB2).HasColumnName("MicronutrientsVitaminB2").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.VitaminB3).HasColumnName("MicronutrientsVitaminB3").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.VitaminB6).HasColumnName("MicronutrientsVitaminB6").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.VitaminB12).HasColumnName("MicronutrientsVitaminB12").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.VitaminC).HasColumnName("MicronutrientsVitaminC").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.VitaminD3).HasColumnName("MicronutrientsVitaminD3").HasPrecision(18, 2);
                    microCfg.Property(fm => fm.VitaminE).HasColumnName("MicronutrientsVitaminE").HasPrecision(18, 2);
                });

            builder
                .OwnsOne(f => f.FoodUnit, unitCfg =>
                {
                    unitCfg.Property(fu => fu.UnitType).HasColumnName("FoodUnitType");
                    unitCfg.Property(fu => fu.DefaultGramsQuantityMultiplier).HasColumnName("FoodDefaultGramsQuantityMultiplier").HasPrecision(18, 2);
                });
        }
    }
}
