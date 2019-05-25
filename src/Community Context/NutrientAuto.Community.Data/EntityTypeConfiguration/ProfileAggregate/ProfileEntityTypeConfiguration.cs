using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using System;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.ProfileAggregate
{
    public class ProfileEntityTypeConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .OwnsOne(p => p.AvatarImage, imageCfg =>
                {
                    imageCfg.Property(i => i.ImageName).IsRequired().HasMaxLength(150).HasColumnName("AvatarImageName");
                    imageCfg.Property(i => i.UrlPath).IsRequired().HasMaxLength(500).HasColumnName("AvatarImageUrlPath");
                });

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .OwnsOne(p => p.EmailAddress, emailAddressCfg =>
                {
                    emailAddressCfg.Property(emailAddress => emailAddress.Email).IsRequired().HasMaxLength(250).HasColumnName("EmailAddress");
                });

            builder
                .Property(p => p.BirthDate)
                .IsRequired();

            builder
                .Property(p => p.Bio)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .OwnsOne(p => p.Settings, settingsCfg =>
                {
                    settingsCfg.Property(settings => settings.PrivacyType).HasColumnName("PrivacyType");
                });

            builder
                .Ignore(p => p.IsPublic);

            builder
                .Ignore(p => p.IsProtected);

            builder
                .Ignore(p => p.IsPrivate);

            builder
                .OwnsMany(p => p.Friends, cfg =>
                {
                    cfg.Property<Guid>("Id");
                    cfg.HasKey("Id");
                    cfg.HasOne<Profile>().WithMany().HasForeignKey(pl => pl.FriendId);
                    cfg.ToTable("Friends");
                });
        }
    }
}
