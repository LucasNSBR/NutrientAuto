using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using System;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.ProfileAggregate
{
    public class FriendEntityTypeConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder
                .Property<Guid>("Id");

            builder
                .HasKey("Id");

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .ToTable("Friends");
        }
    }
}
