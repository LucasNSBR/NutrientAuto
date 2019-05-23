using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using NutrientAuto.Community.Domain.Commands.GoalAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.GoalAggregate;
using NutrientAuto.Community.Domain.Repositories.GoalAggregate;
using NutrientAuto.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.GoalAggregate
{
    public class GoalCommandHandler : ContextCommandHandler,
                                      IRequestHandler<RegisterGoalCommand, CommandResult>,
                                      IRequestHandler<UpdateGoalCommand, CommandResult>,
                                      IRequestHandler<RemoveGoalCommand, CommandResult>,
                                      IRequestHandler<SetCompletedGoalCommand, CommandResult>,
                                      IRequestHandler<SetUncompletedGoalCommand, CommandResult>
    {
        private readonly IGoalRepository _goalRepository;
        private readonly Guid _currentProfileId;

        public GoalCommandHandler(IGoalRepository goalRepository, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<GoalCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _goalRepository = goalRepository;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterGoalCommand request, CancellationToken cancellationToken)
        {
            Goal goal = new Goal(
                _currentProfileId,
                request.Title,
                request.Details
                );

            await _goalRepository.RegisterAsync(goal);

            return await CommitAndPublishAsync(new GoalRegisteredDomainEvent(
                goal.Id,
                goal.ProfileId,
                request.WritePost
                ));
        }

        public async Task<CommandResult> Handle(UpdateGoalCommand request, CancellationToken cancellationToken)
        {
            Goal goal = await _goalRepository.GetByIdAsync(request.GoalId);
            if (!FoundValidGoal(goal))
                return FailureDueToGoalNotFound();

            goal.Update(
                request.Title,
                request.Details
                );

            await _goalRepository.UpdateAsync(goal);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveGoalCommand request, CancellationToken cancellationToken)
        {
            Goal goal = await _goalRepository.GetByIdAsync(request.GoalId);
            if (!FoundValidGoal(goal))
                return FailureDueToGoalNotFound();

            await _goalRepository.RemoveAsync(goal);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(SetCompletedGoalCommand request, CancellationToken cancellationToken)
        {
            Goal goal = await _goalRepository.GetByIdAsync(request.GoalId);
            if (!FoundValidGoal(goal))
                return FailureDueToGoalNotFound();

            goal.SetCompleted(request.DateCompleted, request.AccomplishmentDetails);
            if (!goal.IsValid)
                return FailureDueToEntityStateInconsistency(goal);

            await _goalRepository.UpdateAsync(goal);

            return await CommitAndPublishAsync(new GoalCompletedDomainEvent(
                goal.Id,
                goal.ProfileId,
                request.WritePost
                ));
        }

        public async Task<CommandResult> Handle(SetUncompletedGoalCommand request, CancellationToken cancellationToken)
        {
            Goal goal = await _goalRepository.GetByIdAsync(request.GoalId);
            if (!FoundValidGoal(goal))
                return FailureDueToGoalNotFound();

            goal.SetUncompleted();
            if (!goal.IsValid)
                return FailureDueToEntityStateInconsistency(goal);

            await _goalRepository.UpdateAsync(goal);

            return await CommitAndPublishDefaultAsync();
        }

        private bool FoundValidGoal(Goal goal)
        {
            return goal != null && goal.ProfileId == _currentProfileId;
        }

        private CommandResult FailureDueToGoalNotFound()
        {
            return FailureDueToEntityNotFound("Id do Objetivo inválido", "Falha ao buscar Objetivo no banco de dados.");
        }
    }
}