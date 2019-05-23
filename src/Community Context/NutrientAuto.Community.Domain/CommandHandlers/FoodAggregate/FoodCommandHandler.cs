using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.Commands.FoodAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.FoodAggregate;
using NutrientAuto.Community.Domain.Repositories.FoodTableAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.FoodAggregate
{
    public class FoodCommandHandler : ContextCommandHandler,
                                      IRequestHandler<RegisterFoodCommand, CommandResult>,
                                      IRequestHandler<UpdateFoodCommand, CommandResult>,
                                      IRequestHandler<RemoveFoodCommand, CommandResult>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodTableRepository _foodTableRepository;
        private readonly IMapper _mapper;
        private readonly Guid _currentProfileId;

        public FoodCommandHandler(IFoodRepository foodRepository, IFoodTableRepository foodTableRepository, IIdentityService identityService, IMapper mapper, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<FoodCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _foodRepository = foodRepository;
            _foodTableRepository = foodTableRepository;
            _mapper = mapper;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterFoodCommand request, CancellationToken cancellationToken)
        {
            CustomFood customFood = new CustomFood(
                _currentProfileId,
                request.Name,
                request.Description,
                request.FoodTableId,
                _mapper.Map<MacronutrientTable>(request.Macronutrients),
                _mapper.Map<MicronutrientTable>(request.Micronutrients),
                new FoodUnit(request.UnitType, request.DefaultGramsQuantityMultiplier)
                );

            CustomFoodTable customFoodTable = await _foodTableRepository.GetCustomByIdAsync(request.FoodTableId, _currentProfileId);
            if (customFoodTable == null)
                return FailureDueToCustomFoodTableNotFound();

            await _foodRepository.RegisterAsync(customFood);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
        {
            CustomFood customFood = await _foodRepository.GetCustomByIdAsync(request.FoodId, _currentProfileId);
            if (customFood == null)
                return FailureDueToCustomFoodNotFound();

            CustomFoodTable customFoodTable = await _foodTableRepository.GetCustomByIdAsync(request.FoodTableId, _currentProfileId);
            if (customFoodTable == null)
                return FailureDueToCustomFoodTableNotFound();

            customFood.Update(
                request.Name,
                request.Description,
                request.FoodTableId,
                _mapper.Map<MacronutrientTable>(request.Macronutrients),
                _mapper.Map<MicronutrientTable>(request.Micronutrients),
                new FoodUnit(request.UnitType, request.DefaultGramsQuantityMultiplier)
                );

            await _foodRepository.UpdateAsync(customFood);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveFoodCommand request, CancellationToken cancellationToken)
        {
            CustomFood food = await _foodRepository.GetCustomByIdAsync(request.FoodId, _currentProfileId);
            if (food == null)
                return FailureDueToCustomFoodNotFound();

            await _foodRepository.RemoveAsync(food);

            return await CommitAndPublishDefaultAsync();
        }
    }
}
