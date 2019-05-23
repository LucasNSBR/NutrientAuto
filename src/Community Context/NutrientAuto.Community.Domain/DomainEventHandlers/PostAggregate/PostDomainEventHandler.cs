using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate.Subtypes;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.DietAggregate;
using NutrientAuto.Community.Domain.DomainEvents.GoalAggregate;
using NutrientAuto.Community.Domain.DomainEvents.MeasureAggregate;
using NutrientAuto.Community.Domain.DomainEvents.ProfileAggregate;
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
        private readonly IPostRepository _postRepository;

        public PostDomainEventHandler(IPostRepository postRepository, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<PostDomainEventHandler> logger)
            : base(unitOfWork, logger)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(GoalRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            GoalRegisteredPost goalPost = new GoalRegisteredPost(notification.ProfileId, notification.GoalId);

            await SaveAndCommitAsync(goalPost);
        }

        public async Task Handle(GoalCompletedDomainEvent notification, CancellationToken cancellationToken)
        {
            GoalCompletedPost goalPost = new GoalCompletedPost(notification.ProfileId, notification.GoalId);

            await SaveAndCommitAsync(goalPost);
        }

        public async Task Handle(MeasureRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            MeasureRegisteredPost measurePost = new MeasureRegisteredPost(notification.ProfileId, notification.MeasureId);

            await SaveAndCommitAsync(measurePost);
        }

        public async Task Handle(DietRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            DietRegisteredPost dietPost = new DietRegisteredPost(notification.ProfileId, notification.GoalId);

            await SaveAndCommitAsync(dietPost);
        }

        public async Task Handle(ProfileUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            ProfileUpdatedPost profilePost = new ProfileUpdatedPost(notification.Id);

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
