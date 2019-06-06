using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.DietAggregate;
using NutrientAuto.Community.Domain.DomainEvents.GoalAggregate;
using NutrientAuto.Community.Domain.DomainEvents.MeasureAggregate;
using NutrientAuto.Community.Domain.DomainEvents.ProfileAggregate;
using NutrientAuto.Community.Domain.Factories.PostAggregate;
using NutrientAuto.Community.Domain.Repositories.PostAggregate;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainEventHandlers.PostAggregate
{
    public class PostDomainEventHandler : BaseDomainEventHandler,
                                          INotificationHandler<GoalRegisteredDomainEvent>,
                                          INotificationHandler<GoalCompletedDomainEvent>,
                                          INotificationHandler<MeasureRegisteredDomainEvent>,
                                          INotificationHandler<DietRegisteredDomainEvent>,
                                          INotificationHandler<ProfileUpdatedDomainEvent>
    {
        private readonly IPostFactory _postFactory;
        private readonly IPostRepository _postRepository;

        public PostDomainEventHandler(IPostFactory postFactory, IPostRepository postRepository, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<PostDomainEventHandler> logger)
            : base(unitOfWork, logger)
        {
            _postFactory = postFactory;
            _postRepository = postRepository;
        }

        public async Task Handle(GoalRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            Post goalPost = _postFactory.CreateGoalRegisteredPost(notification.ProfileId, notification.GoalId);

            await SaveAndCommitAsync(goalPost);
        }

        public async Task Handle(GoalCompletedDomainEvent notification, CancellationToken cancellationToken)
        {
            Post goalPost = _postFactory.CreateGoalCompletedPost(notification.ProfileId, notification.GoalId);

            await SaveAndCommitAsync(goalPost);
        }

        public async Task Handle(MeasureRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            Post measurePost = _postFactory.CreateMeasureRegisteredPost(notification.ProfileId, notification.MeasureId);

            await SaveAndCommitAsync(measurePost);
        }

        public async Task Handle(DietRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            Post dietPost = _postFactory.CreateDietRegisteredPost(notification.ProfileId, notification.GoalId);

            await SaveAndCommitAsync(dietPost);
        }

        public async Task Handle(ProfileUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Post profilePost = _postFactory.CreateProfileUpdatedPost(notification.Id);

            await SaveAndCommitAsync(profilePost);
        }

        private async Task SaveAndCommitAsync<TPost>(TPost post) 
            where TPost : Post
        {
            await _postRepository.RegisterAsync(post);
            await CommitAsync();
        }
    }
}
