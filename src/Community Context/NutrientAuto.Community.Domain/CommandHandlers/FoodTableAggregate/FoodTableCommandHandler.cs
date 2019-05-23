using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using NutrientAuto.Community.Domain.Commands.FoodTableAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.FoodTableAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.FoodTableAggregate
{
    public class FoodTableCommandHandler : ContextCommandHandler,
                                           IRequestHandler<RegisterFoodTableCommand, CommandResult>,
                                           IRequestHandler<UpdateFoodTableCommand, CommandResult>,
                                           IRequestHandler<RemoveFoodTableCommand, CommandResult>
    {
        private readonly IFoodTableRepository _foodTableRepository;
        private readonly Guid _currentProfileId;

        public FoodTableCommandHandler(IFoodTableRepository foodTableRepository, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<FoodTableCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _foodTableRepository = foodTableRepository;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterFoodTableCommand request, CancellationToken cancellationToken)
        {
            CustomFoodTable customFoodTable = new CustomFoodTable(
                _currentProfileId,
                request.Name,
                request.Description
                );

            await _foodTableRepository.RegisterAsync(customFoodTable);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(UpdateFoodTableCommand request, CancellationToken cancellationToken)
        {
            CustomFoodTable customFoodTable = await _foodTableRepository.GetCustomByIdAsync(request.FoodTableId, _currentProfileId);
            if (customFoodTable == null)
                return FailureDueToCustomFoodTableNotFound();

            customFoodTable.Update(
                request.Name,
                request.Description
                );

            await _foodTableRepository.UpdateAsync(customFoodTable);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveFoodTableCommand request, CancellationToken cancellationToken)
        {
            CustomFoodTable customFoodTable = await _foodTableRepository.GetCustomByIdAsync(request.FoodTableId, _currentProfileId);
            if (customFoodTable == null)
                return FailureDueToCustomFoodTableNotFound();

            await _foodTableRepository.RemoveAsync(customFoodTable);

            return await CommitAndPublishDefaultAsync();
        }
    }
}
