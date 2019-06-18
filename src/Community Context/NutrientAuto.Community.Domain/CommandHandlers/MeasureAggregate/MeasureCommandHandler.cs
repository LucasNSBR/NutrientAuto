using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureAggregate.BaseCommand;
using NutrientAuto.Community.Domain.Commands.SeedWork;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.Storage.Configuration;
using NutrientAuto.CrossCutting.Storage.Services.StorageService;
using NutrientAuto.CrossCutting.Storage.Services.StorageValidators;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IStorageService _storageService;
        private readonly IOptions<ContainerOptions> _containerOptions;
        private readonly Guid _currentProfileId;

        public MeasureCommandHandler(IMeasureRepository measureRepository, IMeasureCategoryRepository measureCategoryRepository, IMapper mapper, IStorageService storageService, IOptions<ContainerOptions> containerOptions, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<MeasureCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _measureRepository = measureRepository;
            _measureCategoryRepository = measureCategoryRepository;
            _mapper = mapper;
            _storageService = storageService;
            _containerOptions = containerOptions;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterMeasureCommand request, CancellationToken cancellationToken)
        {
            CommandResult commandInvariantResult = await ValidateCommandInvariants(request);
            if (!commandInvariantResult.Success)
                return FailureDueTo(commandInvariantResult.Notifications.ToList());

            BasicMeasure basicMeasure = new BasicMeasure(request.Height, request.Weight);

            List<Image> bodyPictures = new List<Image>();
            foreach (IFormFile bodyPicture in request.BodyPictures)
            {
                StorageResult storageResult = await UploadBodyPictureToStorage(bodyPicture);
                if (!storageResult.Success)
                    return FailureDueToUploadFailure();

                bodyPictures.Add(new Image(storageResult.FileName, storageResult.FileUrl));
            }

            Measure measure = new Measure(
                _currentProfileId,
                request.Title,
                request.Details,
                basicMeasure,
                request.MeasureDate,
                bodyPictures,
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
            CommandResult commandInvariantResult = await ValidateCommandInvariants(request);
            if (!commandInvariantResult.Success)
                return FailureDueTo(commandInvariantResult.Notifications.ToList());

            Measure measure = await _measureRepository.GetByIdAsync(request.MeasureId);
            if (!FoundValidMeasure(measure))
                return FailureDueToMeasureNotFound();

            List<Image> bodyPictures = new List<Image>();

            foreach (IFormFile bodyPicture in request.BodyPictures)
            {
                string containerName = _containerOptions.Value.MeasureImageContainerName;

                StorageFile existingBodyPicture = await _storageService.FindFileAsync(containerName, bodyPicture.FileName);
                if (existingBodyPicture == null)
                {
                    StorageResult storageResult = await UploadBodyPictureToStorage(bodyPicture);
                    if (!storageResult.Success)
                        return FailureDueToUploadFailure();

                    bodyPictures.Add(new Image(storageResult.FileName, storageResult.FileUrl));
                }
                else
                    bodyPictures.Add(new Image(existingBodyPicture.UrlPath, existingBodyPicture.Name));
            }

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

        private async Task<StorageResult> UploadBodyPictureToStorage(IFormFile bodyPicture)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                string containerName = _containerOptions.Value.PostImageContainerName;

                await bodyPicture.CopyToAsync(stream);
                StorageResult result = await _storageService.UploadFileToStorageAsync(containerName, stream, Guid.NewGuid().ToString());

                return result;
            }
        }

        private async Task<CommandResult> ValidateCommandInvariants(BaseMeasureCommand request)
        {
            List<StorageValidatorError> bodyPictureValidationsErrors = ValidateBodyPictures(request.BodyPictures);
            if (bodyPictureValidationsErrors.Any())
                return FailureDueToFileValidationFailure(bodyPictureValidationsErrors);

            if (!EnsureMeasureCategoryIdsIsNotDuplicated(request.MeasureLines))
                return FailureDueToDuplicatedMeasureCategories();

            if (!await EnsureMeasureCategoriesExists(request.MeasureLines))
                return FailureDueToCustomMeasureCategoryNotFound();

            return CommandResult.Ok();
        }

        private List<StorageValidatorError> ValidateBodyPictures(List<IFormFile> bodyPictures)
        {
            List<StorageValidatorError> errors = new List<StorageValidatorError>();

            if (bodyPictures.Count > 5)
                errors.Add(new StorageValidatorError("Erro ao anexar", "O número máximo de imagens dessa medição foi excedido."));

            foreach (IFormFile bodyPicture in bodyPictures)
            {
                StorageValidatorResult storageValidator = new ImageStorageValidator().Validate(bodyPicture);
                if (!storageValidator.Success)
                    errors.AddRange(storageValidator.Errors);
            }

            return errors;
        }

        public bool EnsureMeasureCategoryIdsIsNotDuplicated(List<MeasureLineDto> measureLines)
        {
            bool hasDuplicates = measureLines.GroupBy(ml => ml.MeasureCategoryId)
                .Any(g => g.Count() > 1);

            return !hasDuplicates;
        }

        private async Task<bool> EnsureMeasureCategoriesExists(List<MeasureLineDto> measureLines)
        {
            foreach (MeasureLineDto measureLine in measureLines)
            {
                MeasureCategory measureCategory = await _measureCategoryRepository.GetByIdAndProfileIdAsync(measureLine.MeasureCategoryId, _currentProfileId);
                if (measureCategory == null)
                    return false;
            }

            return true;
        }

        private bool FoundValidMeasure(Measure measure)
        {
            return measure != null && measure.ProfileId == _currentProfileId;
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
