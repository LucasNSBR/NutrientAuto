using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureAggregate;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.MeasureAggregate
{
    public class MeasureCommandHandler : ContextCommandHandler,
                                         IRequestHandler<RegisterMeasureCommand, CommandResult>,
                                         IRequestHandler<UpdateMeasureCommand, CommandResult>,
                                         IRequestHandler<RemoveMeasureCommand, CommandResult>
    {
        private readonly IMeasureRepository _measureRepository;
        private readonly IMeasureCategoryRepository _measureCategoryRepository;
        private readonly IMapper _mapper;
        private readonly Guid _currentProfileId;

        public MeasureCommandHandler(IMeasureRepository measureRepository, IMeasureCategoryRepository measureCategoryRepository, IMapper mapper, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<MeasureCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _measureRepository = measureRepository;
            _measureCategoryRepository = measureCategoryRepository;
            _mapper = mapper;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterMeasureCommand request, CancellationToken cancellationToken)
        {
            if (!EnsureBodyPicturesIsInLimit(request.BodyPictures))
                return FailureDueToExcessiveImages();

            if (!EnsureMeasureCategoryIdsIsNotDuplicated(request.MeasureLines))
                return FailureDueToDuplicatedMeasureCategories();

            if (!await EnsureMeasureCategoriesExists(request.MeasureLines))
                return FailureDueToCustomMeasureCategoryNotFound();

            BasicMeasure basicMeasure = new BasicMeasure(request.Height, request.Weight);

            Measure measure = new Measure(
                _currentProfileId,
                request.Title,
                request.Details,
                basicMeasure,
                request.MeasureDate,
                _mapper.Map<List<Image>>(request.BodyPictures),
                _mapper.Map<List<MeasureLine>>(request.MeasureLines)
                );

            await _measureRepository.RegisterAsync(measure);

            return await CommitAndPublishAsync(new MeasureRegisteredDomainEvent(
                measure.Id,
                measure.ProfileId,
                request.WritePost
                ));
        }

        public async Task<CommandResult> Handle(UpdateMeasureCommand request, CancellationToken cancellationToken)
        {
            Measure measure = await _measureRepository.GetByIdAsync(request.MeasureId);
            if (!FoundValidMeasure(measure))
                return FailureDueToMeasureNotFound();

            if (!EnsureBodyPicturesIsInLimit(request.BodyPictures))
                return FailureDueToExcessiveImages();

            if (!await EnsureMeasureCategoriesExists(request.MeasureLines))
                return FailureDueToCustomMeasureCategoryNotFound();

            BasicMeasure basicMeasure = new BasicMeasure(request.Height, request.Weight);

            measure.Update(
                request.Title,
                request.Details,
                basicMeasure,
                request.MeasureDate,
                _mapper.Map<List<Image>>(request.BodyPictures),
                _mapper.Map<List<MeasureLine>>(request.MeasureLines)
                );

            await _measureRepository.UpdateAsync(measure);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveMeasureCommand request, CancellationToken cancellationToken)
        {
            Measure measure = await _measureRepository.GetByIdAsync(request.MeasureId);
            if (!FoundValidMeasure(measure))
                return FailureDueToMeasureNotFound();

            await _measureRepository.RemoveAsync(measure);

            return await CommitAndPublishDefaultAsync();
        }

        private bool EnsureBodyPicturesIsInLimit(List<ImageDto> imageDtos)
        {
            if (imageDtos.Count > 5)
                return false;

            return true;
        }

        public bool EnsureMeasureCategoryIdsIsNotDuplicated(List<MeasureLineDto> measureLineDtos)
        {
            bool hasDuplicates = measureLineDtos.GroupBy(ml => ml.MeasureCategoryId)
                .Any(g => g.Count() > 0);

            return !hasDuplicates;
        }

        private async Task<bool> EnsureMeasureCategoriesExists(List<MeasureLineDto> measureLineDtos)
        {
            foreach (MeasureLineDto measureLineDto in measureLineDtos)
            {
                MeasureCategory measureCategory = await _measureCategoryRepository.GetByIdAndProfileIdAsync(measureLineDto.MeasureCategoryId, _currentProfileId);
                if (measureCategory == null)
                    return false;
            }

            return true;
        }

        private bool FoundValidMeasure(Measure measure)
        {
            return measure != null && measure.ProfileId == _currentProfileId;
        }

        private CommandResult FailureDueToExcessiveImages()
        {
            return FailureDueTo("Erro ao anexar", "O número máximo de imagens dessa medição foi excedido.");
        }

        private CommandResult FailureDueToDuplicatedMeasureCategories()
        {
            return FailureDueToEntityNotFound("Categorias duplicatas", "Não é possível prosseguir com as categorias de medição duplicadas.");
        }

        private CommandResult FailureDueToMeasureNotFound()
        {
            return FailureDueToEntityNotFound("Id da medição inválido", "Falha ao buscar medição no banco de dados.");
        }
    }
}
