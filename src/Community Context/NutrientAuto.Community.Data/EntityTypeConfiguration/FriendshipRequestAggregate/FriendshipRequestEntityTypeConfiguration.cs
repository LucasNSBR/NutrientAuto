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
                .HasForeignKey(fr => fr.RequesterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(fr => fr.RequestedId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(fr => fr.DateCreated)
                .IsRequired();

            builder
                .Ignore(fr => fr.IsPending);

            builder
                .Ignore(fr => fr.IsAccepted);

            builder
                .Ignore(fr => fr.IsRejected);

            builder
                .Ignore(fr => fr.IsCanceled);

            builder
                .Ignore(fr => fr.IsDumped);

            builder
                .Property(fr => fr.RequestBody)
                .HasMaxLength(250);

            builder
                .Property(fr => fr.DateModified)
                .IsRequired();
        }
    }
}
