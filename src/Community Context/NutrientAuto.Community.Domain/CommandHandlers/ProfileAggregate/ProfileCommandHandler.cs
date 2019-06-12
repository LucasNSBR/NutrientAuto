using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Commands.ProfileAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.ProfileAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.Storage.Services.StorageService;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.IO;
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
        private readonly IStorageService _storageService;
        private readonly Guid _currentProfileId;

        public ProfileCommandHandler(IProfileRepository profileRepository, IProfileDomainService profileDomainService, IStorageService storageService, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<ProfileCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _profileRepository = profileRepository;
            _profileDomainService = profileDomainService;
            _storageService = storageService;
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
                new EmailAddress(request.EmailAddress.Email),
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
            Profile profile = await _profileRepository.GetByIdAsync(request.ProfileId);
            if (!FoundValidProfile(profile))
                return FailureDueToProfileNotFound();

            Image image = null;

            using (MemoryStream stream = new MemoryStream())
            {
                await request.AvatarImage.CopyToAsync(stream);
                StorageResult result = await _storageService.UploadFileToStorageAsync(stream, $"profile-photo-{Guid.NewGuid().ToString()}");
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

            return await CommitAndPublishDefaultAsync();
        }

        private bool FoundValidProfile(Profile profile)
        {
            return profile != null && profile.Id == _currentProfileId;
        }
    }
}
