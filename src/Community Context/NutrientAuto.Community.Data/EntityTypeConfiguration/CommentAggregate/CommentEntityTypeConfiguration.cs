using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.CommentAggregate;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.CommentAggregate
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasOne<Post>()
                .WithMany()
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(c => c.ProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(c => c.Body)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(c => c.DateCreated)
                .IsRequired();

            builder
                .HasMany(c => c.Replies)
                .WithOne()
                .HasForeignKey(c => c.ReplyTo)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
