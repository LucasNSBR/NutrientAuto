using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.DietAggregate;
using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Community.Domain.Commands.DietAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.DietAggregate;
using NutrientAuto.Community.Domain.Repositories.DietAggregate;
using NutrientAuto.Community.Domain.Repositories.MealAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.DietAggregate
{
    public class DietCommandHandler : ContextCommandHandler,
                                      IRequestHandler<RegisterDietCommand, CommandResult>,
                                      IRequestHandler<UpdateDietCommand, CommandResult>,
                                      IRequestHandler<RemoveDietCommand, CommandResult>,
                                      IRequestHandler<AddDietMealCommand, CommandResult>,
                                      IRequestHandler<RemoveDietMealCommand, CommandResult>
    {
        private readonly IDietRepository _dietRepository;
        private readonly IMealRepository _mealRepository;
        private readonly Guid _currentProfileId;

        public DietCommandHandler(IDietRepository dietRepository, IMealRepository mealRepository, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<DietCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _dietRepository = dietRepository;
            _mealRepository = mealRepository;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterDietCommand request, CancellationToken cancellationToken)
        {
            Diet diet = new Diet(
                _currentProfileId,
                request.Name,
                request.Description
                );

            await _dietRepository.RegisterAsync(diet);

            return await CommitAndPublishAsync(new DietRegisteredDomainEvent(
                diet.Id,
                diet.ProfileId,
                request.WritePost
                ));
        }

        public async Task<CommandResult> Handle(UpdateDietCommand request, CancellationToken cancellationToken)
        {
            Diet diet = await _dietRepository.GetByIdAsync(request.DietId);
            if (!FoundValidDiet(diet))
                return FailureDueToDietNotFound();

            diet.Update(
                request.Name, 
                request.Description
                );
            
            await _dietRepository.UpdateAsync(diet);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveDietCommand request, CancellationToken cancellationToken)
        {
            Diet diet = await _dietRepository.GetByIdAsync(request.DietId);
            if (!FoundValidDiet(diet))
                return FailureDueToDietNotFound();

            await _dietRepository.RemoveAsync(diet);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(AddDietMealCommand request, CancellationToken cancellationToken)
        {
            Diet diet = await _dietRepository.GetByIdAsync(request.DietId);
            if (!FoundValidDiet(diet))
                return FailureDueToEntityNotFound();

            Time timeOfDay = new Time(request.TimeOfDay.Hour, request.TimeOfDay.Minute, request.TimeOfDay.Second);

            diet.AddMeal(request.Name, request.Description, timeOfDay);
            if (!diet.IsValid)
                return FailureDueToEntityStateInconsistency(diet);

            await _dietRepository.UpdateAsync(diet);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveDietMealCommand request, CancellationToken cancellationToken)
        {
            Diet diet = await _dietRepository.GetByIdAsync(request.DietId);
            if (!FoundValidDiet(diet))
                return FailureDueToDietNotFound();

            Meal meal = diet.FindMeal(request.DietMealId);

            diet.RemoveMeal(meal);
            if (!diet.IsValid)
                return FailureDueToEntityStateInconsistency(diet);

            await _mealRepository.RemoveAsync(meal);
            await _dietRepository.UpdateAsync(diet);

            return await CommitAndPublishDefaultAsync();
        }

        private bool FoundValidDiet(Diet diet)
        {
            return diet != null && diet.ProfileId == _currentProfileId;
        }
    }
}
