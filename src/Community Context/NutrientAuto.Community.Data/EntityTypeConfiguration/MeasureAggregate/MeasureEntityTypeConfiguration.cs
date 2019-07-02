using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Shared.Data.Extensions;
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
                .HasForeignKey(m => m.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

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
                    basicMeasureCfg.Property(m => m.Height).HasColumnName("Height").HasPrecision(18, 2);
                    basicMeasureCfg.Property(m => m.Weight).HasColumnName("Weight").HasPrecision(18, 2);
                    basicMeasureCfg.Property(m => m.Bmi).HasColumnName("Bmi").HasPrecision(18, 2);
                });

            builder
                .Property(m => m.MeasureDate)
                .IsRequired();

            builder
                .OwnsMany(m => m.BodyPictures, bodyPictureCfg =>
                {
                    bodyPictureCfg.Property<Guid>("Id");
                    bodyPictureCfg.HasKey("Id");
                    bodyPictureCfg.Property(i => i.ImageName).IsRequired().HasMaxLength(150).HasColumnName("BodyPictureImageName");
                    bodyPictureCfg.Property(i => i.UrlPath).IsRequired().HasMaxLength(500).HasColumnName("BodyPictureImageUrlPath");
                    bodyPictureCfg.OnDelete(DeleteBehavior.Cascade);
                    bodyPictureCfg.ToTable("MeasureBodyPictures");
                });

            builder
                .OwnsMany(m => m.MeasureLines, measureLineCfg =>
                {
                    measureLineCfg.Property<Guid>("Id");
                    measureLineCfg.HasKey("Id");
                    measureLineCfg.HasOne(m => m.MeasureCategory).WithMany().HasForeignKey(ml => ml.MeasureCategoryId).OnDelete(DeleteBehavior.Restrict);
                    measureLineCfg.Property(m => m.MeasureCategoryId).HasColumnName("MeasureCategoryId");
                    measureLineCfg.Property(m => m.Value).HasColumnName("Value").HasPrecision(18, 2);
                    measureLineCfg.OnDelete(DeleteBehavior.Cascade);
                    measureLineCfg.ToTable("MeasureLines");
                });
        }
    }
}
