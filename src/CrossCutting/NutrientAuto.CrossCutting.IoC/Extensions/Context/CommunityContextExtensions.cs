using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Data.Repositories.CommentAggregate;
using NutrientAuto.Community.Data.Repositories.DietAggregate;
using NutrientAuto.Community.Data.Repositories.FoodAggregate;
using NutrientAuto.Community.Data.Repositories.FoodTableAggregate;
using NutrientAuto.Community.Data.Repositories.FriendshipRequestAggregate;
using NutrientAuto.Community.Data.Repositories.GoalAggregate;
using NutrientAuto.Community.Data.Repositories.MealAggregate;
using NutrientAuto.Community.Data.Repositories.MeasureAggregate;
using NutrientAuto.Community.Data.Repositories.MeasureCategoryAggregate;
using NutrientAuto.Community.Data.Repositories.PostAggregate;
using NutrientAuto.Community.Data.Repositories.ProfileAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.CommentAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.DietAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.FoodAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.FoodTableAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.GoalAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.MealAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.MeasureAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.PostAggregate;
using NutrientAuto.Community.Domain.CommandHandlers.ProfileAggregate;
using NutrientAuto.Community.Domain.Commands.CommentAggregate;
using NutrientAuto.Community.Domain.Commands.DietAggregate;
using NutrientAuto.Community.Domain.Commands.FoodAggregate;
using NutrientAuto.Community.Domain.Commands.FoodTableAggregate;
using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Commands.GoalAggregate;
using NutrientAuto.Community.Domain.Commands.MealAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Commands.PostAggregate;
using NutrientAuto.Community.Domain.Commands.ProfileAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEventHandlers.DietAggregate;
using NutrientAuto.Community.Domain.DomainEventHandlers.PostAggregate;
using NutrientAuto.Community.Domain.DomainEvents.DietAggregate;
using NutrientAuto.Community.Domain.DomainEvents.GoalAggregate;
using NutrientAuto.Community.Domain.DomainEvents.MealAggregate;
using NutrientAuto.Community.Domain.DomainEvents.MeasureAggregate;
using NutrientAuto.Community.Domain.DomainEvents.ProfileAggregate;
using NutrientAuto.Community.Domain.DomainServices.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.DomainServices.MeasureStatisticsAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.Factories.PostAggregate;
using NutrientAuto.Community.Domain.Repositories.CommentAggregate;
using NutrientAuto.Community.Domain.Repositories.DietAggregate;
using NutrientAuto.Community.Domain.Repositories.FoodAggregate;
using NutrientAuto.Community.Domain.Repositories.FoodTableAggregate;
using NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Repositories.GoalAggregate;
using NutrientAuto.Community.Domain.Repositories.MealAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Repositories.PostAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.Settings.Community;
using System;

namespace NutrientAuto.CrossCutting.IoC.Extensions.Context
{
    public static partial class ContextDependencyInjectionExtensions
    {
        public static IServiceCollection AddCommunityContext(this IServiceCollection services, IConfiguration configuration, Action<CommunityDefaultOptions> communityDefaultOptions)
        {
            services.Configure(communityDefaultOptions);

            services.AddDbContext<CommunityDbContext>(opt =>
                 opt.UseSqlServer(configuration.GetConnectionString("SqlServerMain")));

            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProfileReadModelRepository, ProfileReadModelRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IFoodTableRepository, FoodTableRepository>();
            services.AddScoped<IMealRepository, MealRepository>();
            services.AddScoped<IMealReadModelRepository, MealReadModelRepository>();
            services.AddScoped<IDietRepository, DietRepository>();
            services.AddScoped<IDietReadModelRepository, DietReadModelRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostReadModelRepository, PostReadModelRepository>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IGoalReadModelRepository, GoalReadModelRepository>();
            services.AddScoped<IMeasureRepository, MeasureRepository>();
            services.AddScoped<IMeasureReadModelRepository, MeasureReadModelRepository>();
            services.AddScoped<IMeasureCategoryRepository, MeasureCategoryRepository>();

            services.AddScoped<IFriendshipRequestRepository, FriendshipRequestRepository>();
            services.AddScoped<IFriendshipRequestReadModelRepository, FriendshipRequestReadModelRepository>();

            services.AddScoped<IProfileDomainService, ProfileDomainService>();
            services.AddScoped<IFriendshipRequestDomainService, FriendshipRequestDomainService>();
            services.AddScoped<IMeasureStatisticsDomainService, MeasureStatisticsDomainService>();

            services.AddScoped<IRequestHandler<UpdateProfileCommand, CommandResult>, ProfileCommandHandler>();
            services.AddScoped<IRequestHandler<SetAvatarImageCommand, CommandResult>, ProfileCommandHandler>();
            services.AddScoped<IRequestHandler<ChangeSettingsCommand, CommandResult>, ProfileCommandHandler>();
            services.AddScoped<IRequestHandler<UnfriendProfileCommand, CommandResult>, ProfileCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterPostCommand, CommandResult>, PostCommandHandler>();
            services.AddScoped<IRequestHandler<RemovePostCommand, CommandResult>, PostCommandHandler>();
            services.AddScoped<IRequestHandler<AddLikeCommand, CommandResult>, PostCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveLikeCommand, CommandResult>, PostCommandHandler>();
            services.AddScoped<IRequestHandler<AddCommentCommand, CommandResult>, PostCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCommentCommand, CommandResult>, PostCommandHandler>();

            services.AddScoped<IRequestHandler<ReplyCommentCommand, CommandResult>, CommentCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterMeasureCommand, CommandResult>, MeasureCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateMeasureCommand, CommandResult>, MeasureCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveMeasureCommand, CommandResult>, MeasureCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterMeasureCategoryCommand, CommandResult>, MeasureCategoryCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateMeasureCategoryCommand, CommandResult>, MeasureCategoryCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveMeasureCategoryCommand, CommandResult>, MeasureCategoryCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterGoalCommand, CommandResult>, GoalCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateGoalCommand, CommandResult>, GoalCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveGoalCommand, CommandResult>, GoalCommandHandler>();
            services.AddScoped<IRequestHandler<SetCompletedGoalCommand, CommandResult>, GoalCommandHandler>();
            services.AddScoped<IRequestHandler<SetUncompletedGoalCommand, CommandResult>, GoalCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterFriendshipRequestCommand, CommandResult>, FriendshipRequestCommandHandler>();
            services.AddScoped<IRequestHandler<AcceptFriendshipRequestCommand, CommandResult>, FriendshipRequestCommandHandler>();
            services.AddScoped<IRequestHandler<RejectFriendshipRequestCommand, CommandResult>, FriendshipRequestCommandHandler>();
            services.AddScoped<IRequestHandler<CancelFriendshipRequestCommand, CommandResult>, FriendshipRequestCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterFoodCommand, CommandResult>, FoodCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateFoodCommand, CommandResult>, FoodCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveFoodCommand, CommandResult>, FoodCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterFoodTableCommand, CommandResult>, FoodTableCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateFoodTableCommand, CommandResult>, FoodTableCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveFoodTableCommand, CommandResult>, FoodTableCommandHandler>();

            services.AddScoped<IRequestHandler<RegisterDietCommand, CommandResult>, DietCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateDietCommand, CommandResult>, DietCommandHandler>();
            services.AddScoped<IRequestHandler<AddDietMealCommand, CommandResult>, DietCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveDietMealCommand, CommandResult>, DietCommandHandler>();

            services.AddScoped<IRequestHandler<UpdateMealCommand, CommandResult>, MealCommandHandler>();
            services.AddScoped<IRequestHandler<AddMealFoodCommand, CommandResult>, MealCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveMealFoodCommand, CommandResult>, MealCommandHandler>();

            services.AddScoped<INotificationHandler<ProfileUpdatedDomainEvent>, PostDomainEventHandler>();
            services.AddScoped<INotificationHandler<GoalRegisteredDomainEvent>, PostDomainEventHandler>();
            services.AddScoped<INotificationHandler<GoalCompletedDomainEvent>, PostDomainEventHandler>();
            services.AddScoped<INotificationHandler<MeasureRegisteredDomainEvent>, PostDomainEventHandler>();
            services.AddScoped<INotificationHandler<DietRegisteredDomainEvent>, PostDomainEventHandler>();

            services.AddScoped<INotificationHandler<MealFoodAddedDomainEvent>, DietDomainEventHandler>();
            services.AddScoped<INotificationHandler<MealFoodRemovedDomainEvent>, DietDomainEventHandler>();

            services.AddScoped<IPostFactory, PostFactory>();

            services.AddScoped<ICommunityDbContext, CommunityDbContext>(provider => provider.GetRequiredService<CommunityDbContext>());
            services.AddScoped<IUnitOfWork<ICommunityDbContext>, UnitOfWork<ICommunityDbContext>>();

            return services;
        }
    }
}
