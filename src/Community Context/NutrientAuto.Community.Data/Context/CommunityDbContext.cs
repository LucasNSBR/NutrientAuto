using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.EntityTypeConfiguration.CommentAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.DietAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.FoodAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.FoodTableAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.FriendshipRequestAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.GoalAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.MealAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.MeasureAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.MeasureCategoryAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.PostAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.ProfileAggregate;
using NutrientAuto.Community.Data.EntityTypeConfiguration.ReminderAggregate;
using NutrientAuto.Community.Domain.Aggregates.CommentAggregate;
using NutrientAuto.Community.Domain.Aggregates.DietAggregate;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Aggregates.ReminderAggregate;
using NutrientAuto.Community.Domain.Context;

namespace NutrientAuto.Community.Data.Context
{
    public class CommunityDbContext : DbContext, ICommunityDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<MeasureCategory> MeasureCategories { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<FriendshipRequest> FriendshipRequests { get; set; }
        public DbSet<Diet> Diets { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodTable> FoodTables { get; set; }

        public CommunityDbContext(DbContextOptions<CommunityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProfileEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FriendEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GoalEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeasureEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MeasureCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReminderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomMeasureCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipRequestEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DietEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MealEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FoodEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomFoodEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FoodTableEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomFoodTableEntityTypeConfiguration());
        }
    }
}
