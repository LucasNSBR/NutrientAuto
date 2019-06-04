using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate.Subtypes;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using System;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.PostAggregate
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(p => p.ProfileId);

            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.Body)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .OwnsOne(p => p.AttachedImage, imageCfg =>
                {
                    imageCfg.Property(i => i.ImageName).IsRequired().HasMaxLength(150).HasColumnName("PostImageName");
                    imageCfg.Property(i => i.UrlPath).IsRequired().HasMaxLength(500).HasColumnName("PostImageUrlPath");
                });

            builder.HasDiscriminator<int>("PostType")
                .HasValue<Post>(0)
                .HasValue<ProfileUpdatedPost>(1)
                .HasValue<GoalRegisteredPost>(2)
                .HasValue<GoalCompletedPost>(3)
                .HasValue<MeasureRegisteredPost>(4)
                .HasValue<DietRegisteredPost>(5);

            builder
                .OwnsOne(p => p.EntityReference, entityReferenceCfg =>
                {
                    entityReferenceCfg.Property(r => r.HasReference).HasColumnName("HasEntityReference");
                    entityReferenceCfg.Property(r => r.ReferenceId).HasColumnName("EntityReferenceId");
                    entityReferenceCfg.Property(r => r.ReferenceType).HasColumnName("EntityReferenceType");
                });

            builder
                .Property(p => p.DateCreated)
                .IsRequired();

            builder
                .OwnsMany(p => p.Likes, cfg =>
                {
                    cfg.Property<Guid>("Id");
                    cfg.HasKey("Id");
                    cfg.HasOne<Profile>().WithMany().HasForeignKey(pl => pl.ProfileId);
                    cfg.Property(p => p.DateCreated).IsRequired();
                    cfg.ToTable("PostLikes");
                });
        }
    }
}
