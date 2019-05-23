using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.MeasureCategoryAggregate
{
    public class MeasureCategoryCommandHandler : ContextCommandHandler,
                                           IRequestHandler<RegisterMeasureCategoryCommand, CommandResult>,
                                           IRequestHandler<UpdateMeasureCategoryCommand, CommandResult>,
                                           IRequestHandler<RemoveMeasureCategoryCommand, CommandResult>
    {
        private readonly IMeasureCategoryRepository _measureCategoryRepository;
        private readonly Guid _currentProfileId;

        public MeasureCategoryCommandHandler(IMeasureCategoryRepository measureCategory, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<MeasureCategoryCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _measureCategoryRepository = measureCategory;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterMeasureCategoryCommand request, CancellationToken cancellationToken)
        {
            CustomMeasureCategory customMeasureCategory = new CustomMeasureCategory(
                _currentProfileId,
                request.Name,
                request.Description,
                request.IsFavorite
                );

            await _measureCategoryRepository.RegisterAsync(customMeasureCategory);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(UpdateMeasureCategoryCommand request, CancellationToken cancellationToken)
        {
            CustomMeasureCategory customMeasureCategory = await _measureCategoryRepository.GetCustomByIdAsync(request.MeasureCategoryId, _currentProfileId);
            if (customMeasureCategory == null)
                return FailureDueToCustomMeasureCategoryNotFound();

            customMeasureCategory.Update(
                request.Name,
                request.Description,
                request.IsFavorite
                );

            await _measureCategoryRepository.UpdateAsync(customMeasureCategory);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveMeasureCategoryCommand request, CancellationToken cancellationToken)
        {
            CustomMeasureCategory customMeasureCategory = await _measureCategoryRepository.GetCustomByIdAsync(request.MeasureCategoryId, _currentProfileId);
            if (customMeasureCategory == null)
                return FailureDueToCustomMeasureCategoryNotFound();

            await _measureCategoryRepository.RemoveAsync(customMeasureCategory);

            return await CommitAndPublishDefaultAsync();
        }
    }
}
