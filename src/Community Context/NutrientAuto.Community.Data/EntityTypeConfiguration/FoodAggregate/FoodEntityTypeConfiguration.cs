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
                .OwnsOne(f => f.Macronutrients, macroCfg =>
                {
                    macroCfg.Property(fm => fm.Kcal).HasColumnName("FoodKcal");
                    macroCfg.Property(fm => fm.Kj).HasColumnName("FoodKj");
                    macroCfg.Property(fm => fm.Protein).HasColumnName("FoodProtein");
                    macroCfg.Property(fm => fm.Carbohydrate).HasColumnName("FoodCarbohydrate");
                    macroCfg.Property(fm => fm.Fat).HasColumnName("FoodFat");
                });

            builder
                .OwnsOne(f => f.Micronutrients, microCfg =>
                {
                    microCfg.Property(fm => fm.Calcium).HasColumnName("MicronutrientsCalcium");
                    microCfg.Property(fm => fm.Chromium).HasColumnName("MicronutrientsChromium");
                    microCfg.Property(fm => fm.Copper).HasColumnName("MicronutrientsCopper");
                    microCfg.Property(fm => fm.Magnesium).HasColumnName("MicronutrientsMagnesium");
                    microCfg.Property(fm => fm.Manganese).HasColumnName("MicronutrientsManganese");
                    microCfg.Property(fm => fm.Phosphorus).HasColumnName("MicronutrientsPhosphorus");
                    microCfg.Property(fm => fm.Potassium).HasColumnName("MicronutrientsPotassium");
                    microCfg.Property(fm => fm.Sodium).HasColumnName("MicronutrientsSodium");
                    microCfg.Property(fm => fm.Selenium).HasColumnName("MicronutrientsSelenium");
                    microCfg.Property(fm => fm.Zinc).HasColumnName("MicronutrientsZinc");
                    microCfg.Property(fm => fm.VitaminB1).HasColumnName("MicronutrientsVitaminB1");
                    microCfg.Property(fm => fm.VitaminB2).HasColumnName("MicronutrientsVitaminB2");
                    microCfg.Property(fm => fm.VitaminB3).HasColumnName("MicronutrientsVitaminB3");
                    microCfg.Property(fm => fm.VitaminB6).HasColumnName("MicronutrientsVitaminB6");
                    microCfg.Property(fm => fm.VitaminB12).HasColumnName("MicronutrientsVitaminB12");
                    microCfg.Property(fm => fm.VitaminC).HasColumnName("MicronutrientsVitaminC");
                    microCfg.Property(fm => fm.VitaminD3).HasColumnName("MicronutrientsVitaminD3");
                    microCfg.Property(fm => fm.VitaminE).HasColumnName("MicronutrientsVitaminE");
                });

            builder
                .OwnsOne(f => f.FoodUnit, unitCfg =>
                {
                    unitCfg.Property(fu => fu.UnitType).HasColumnName("FoodUnitType");
                    unitCfg.Property(fu => fu.DefaultGramsQuantityMultiplier).HasColumnName("FoodDefaultGramsQuantityMultiplier");
                });
        }
    }
}
