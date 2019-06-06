using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Aggregates.ReminderAggregate;
using NutrientAuto.Shared.Data.EntityTypeConfiguration.ValueObjects;

namespace NutrientAuto.Community.Data.EntityTypeConfiguration.ReminderAggregate
{
    public class ReminderEntityTypeConfiguration : IEntityTypeConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            builder
                .HasKey(k => k.Id);

            builder
                .HasOne<Profile>()
                .WithMany()
                .HasForeignKey(r => r.ProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(r => r.Details)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .OwnsOne(r => r.TimeOfDay, cfg =>
                {
                    new TimeValueObjectConfiguration().Configure(cfg);
                });
        }
    }
}
