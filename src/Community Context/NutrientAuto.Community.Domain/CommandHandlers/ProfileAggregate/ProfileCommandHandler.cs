using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Commands.ProfileAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.ProfileAggregate;
using NutrientAuto.Community.Domain.DomainServices.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.Storage.Configuration;
using NutrientAuto.CrossCutting.Storage.Services.StorageService;
using NutrientAuto.CrossCutting.Storage.Services.StorageValidators;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.ProfileAggregate
{
    public class ProfileCommandHandler : ContextCommandHandler,
                                         IRequestHandler<UpdateProfileCommand, CommandResult>,
                                         IRequestHandler<SetAvatarImageCommand, CommandResult>,
                                         IRequestHandler<ChangeSettingsCommand, CommandResult>,
                                         IRequestHandler<UnfriendProfileCommand, CommandResult>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly IFriendshipRequestDomainService _friendshipRequestDomainService;
        private readonly IStorageService _storageService;
        private readonly IOptions<ContainerOptions> _containerOptions;
        private readonly Guid _currentProfileId;

        public ProfileCommandHandler(IProfileRepository profileRepository, IProfileDomainService profileDomainService, IFriendshipRequestDomainService friendshipRequestDomainService, IStorageService storageService, IOptions<ContainerOptions> containerOptions, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<ProfileCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _profileRepository = profileRepository;
            _profileDomainService = profileDomainService;
            _friendshipRequestDomainService = friendshipRequestDomainService;
            _storageService = storageService;
            _containerOptions = containerOptions;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            Profile profile = await _profileRepository.GetByIdAsync(request.ProfileId);
            if (!FoundValidProfile(profile))
                return FailureDueToProfileNotFound();

            profile.Update(
                request.Genre,
                request.Name,
                request.Username,
                request.BirthDate,
                request.Bio
                );

            await _profileRepository.UpdateAsync(profile);

            return await CommitAndPublishAsync(new ProfileUpdatedDomainEvent(
                profile.Id,
                request.WritePost
                ));
        }

        public async Task<CommandResult> Handle(SetAvatarImageCommand request, CancellationToken cancellationToken)
        {
            StorageValidatorResult storageValidator = new ImageStorageValidator().Validate(request.AvatarImage);
            if (!storageValidator.Success)
                return FailureDueToFileValidationFailure(storageValidator.Errors.ToList());

            Profile profile = await _profileRepository.GetByIdAsync(request.ProfileId);
            if (!FoundValidProfile(profile))
                return FailureDueToProfileNotFound();

            Image image = null;

            using (MemoryStream stream = new MemoryStream())
            {
                string containerName = _containerOptions.Value.ProfileImageContainerName;

                await request.AvatarImage.CopyToAsync(stream);
                StorageResult result = await _storageService.UploadFileToStorageAsync(containerName, stream, Guid.NewGuid().ToString());
                if (!result.Success)
                    return FailureDueToUploadFailure();

                image = new Image(result.FileUrl, result.FileName);
            }

            profile.SetAvatarImage(image);

            await _profileRepository.UpdateAsync(profile);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(ChangeSettingsCommand request, CancellationToken cancellationToken)
        {
            Profile profile = await _profileRepository.GetByIdAsync(request.ProfileId);
            if (!FoundValidProfile(profile))
                return FailureDueToProfileNotFound();

            ProfileSettings settings = new ProfileSettings(request.PrivacyType);

            profile.ChangeSettings(settings);

            await _profileRepository.UpdateAsync(profile);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(UnfriendProfileCommand request, CancellationToken cancellationToken)
        {
            CommandResult unfriendResult = await _profileDomainService.EndFriendship(_currentProfileId, request.FriendProfileId);
            if (!unfriendResult.Success)
                return unfriendResult;

            CommandResult friendshipDumpResult = await _friendshipRequestDomainService.DumpExistingFriendshipRequest(_currentProfileId, request.FriendProfileId);
            if (!friendshipDumpResult.Success)
                return friendshipDumpResult;

            return await CommitAndPublishDefaultAsync();
        }

        private bool FoundValidProfile(Profile profile)
        {
            return profile != null && profile.Id == _currentProfileId;
        }
    }
}
