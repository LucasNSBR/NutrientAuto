using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.FriendshipRequestAggregate
{
    public class FriendshipRequestEntityTypeConfiguration : IEntityTypeConfiguration<FriendshipRequest>
    {
        public void Configure(EntityTypeBuilder<FriendshipRequest> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(fr => fr.RequesterId);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(fr => fr.RequestedId);

            builder
                .Property(fr => fr.DateCreated)
                .IsRequired();

            builder
                .Ignore(fr => fr.IsPending);

            builder
                .Ignore(fr => fr.IsAccepted);

            builder
                .Property(fr => fr.RequestBody)
                .HasMaxLength(250);
        }
    }
}
