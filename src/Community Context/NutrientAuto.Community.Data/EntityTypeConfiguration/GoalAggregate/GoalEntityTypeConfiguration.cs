using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.GoalAggregate
{
    public class GoalEntityTypeConfiguration : IEntityTypeConfiguration<Goal>
    {
        public void Configure(EntityTypeBuilder<Goal> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(g => g.ProfileId);

            builder
                .Property(g => g.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .OwnsOne(g => g.Status, cfg =>
                {
                    cfg.Property(gs => gs.IsCompleted).HasColumnName("IsCompleted");
                    cfg.Property(gs => gs.DateCompleted).HasColumnName("DateCompleted");
                    cfg.Property(gs => gs.AccomplishmentDetails).HasMaxLength(500).HasColumnName("AccomplishmentDetails");
                });

            builder
                .Property(g => g.Details)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(g => g.DateCreated)
                .IsRequired();
        }
    }
}
