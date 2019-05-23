using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using System;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.MeasureAggregate
{
    public class MeasureEntityTypeConfiguration : IEntityTypeConfiguration<Measure>
    {
        public void Configure(EntityTypeBuilder<Measure> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(m => m.ProfileId);

            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.Details)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .OwnsOne(m => m.BasicMeasure, basicMeasureCfg =>
                {
                    basicMeasureCfg.Property(m => m.Height).HasColumnName("Height");
                    basicMeasureCfg.Property(m => m.Weight).HasColumnName("Weight");
                    basicMeasureCfg.Property(m => m.Bmi).HasColumnName("Bmi");
                });

            builder
                .Property(m => m.MeasureDate)
                .IsRequired();

            builder
                .OwnsMany(m => m.BodyPictures, bodyPictureCfg =>
                {
                    bodyPictureCfg.Property<Guid>("Id");
                    bodyPictureCfg.HasKey("Id");
                    bodyPictureCfg.Property(i => i.Name).IsRequired().HasMaxLength(150).HasColumnName("BodyPictureImageName");
                    bodyPictureCfg.Property(i => i.UrlPath).IsRequired().HasMaxLength(500).HasColumnName("BodyPictureImageUrlPath");
                });

            builder
                .OwnsMany(m => m.MeasureLines, cfg =>
                {
                    cfg.Property<Guid>("Id");
                    cfg.HasKey("Id");
                    cfg.HasOne(m => m.MeasureCategory).WithMany().HasForeignKey(ml => ml.MeasureCategoryId);
                });
        }
    }
}
