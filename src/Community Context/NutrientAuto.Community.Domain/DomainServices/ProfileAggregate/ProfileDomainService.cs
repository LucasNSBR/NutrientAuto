using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using NutrientAuto.Shared.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainServices.ProfileAggregate
{
    public class ProfileDomainService : IProfileDomainService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileDomainService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<bool> CanAccessProfileData(Guid requesterId, Guid requestedId)
        {
            Profile requestedProfile = await _profileRepository.GetByIdAsync(requestedId);

            if (requestedProfile != null)
            {
                if (requestedProfile.IsPublic)
                    return true;
                if (requestedProfile.IsProtected)
                    return requestedProfile.IsFriend(requesterId);
            }

            return false;
        }

        public async Task<CommandResult> MakeFriends(Guid requesterId, Guid requestedId)
        {
            Profile requesterProfile = await _profileRepository.GetByIdAsync(requesterId);
            Profile requestedProfile = await _profileRepository.GetByIdAsync(requestedId);

            if (!FoundValidProfiles(requesterProfile, requestedProfile))
                return FailureDueToProfilesNotFound();

            requesterProfile.AddFriend(requestedProfile);
            requestedProfile.AddFriend(requesterProfile);

            return await CheckValidationAndUpdateAsync(requesterProfile, requestedProfile);
        }

        public async Task<CommandResult> EndFriendship(Guid requesterId, Guid requestedId)
        {
            Profile requesterProfile = await _profileRepository.GetByIdAsync(requesterId);
            Profile requestedProfile = await _profileRepository.GetByIdAsync(requestedId);

            if (!FoundValidProfiles(requesterProfile, requestedProfile))
                return FailureDueToProfilesNotFound();

            requesterProfile.RemoveFriend(requestedProfile);
            requestedProfile.RemoveFriend(requesterProfile);

            return await CheckValidationAndUpdateAsync(requesterProfile, requestedProfile);
        }

        private bool FoundValidProfiles(Profile requesterProfile, Profile requestedProfile)
        {
            return requesterProfile != null && requestedProfile != null;
        }

        private CommandResult FailureDueToProfilesNotFound()
        {
            return CommandResult.Failure("Perfis inválidos", "Ocorreu um erro ao buscar os perfis envolvidos na operação.");
        }

        private async Task<CommandResult> CheckValidationAndUpdateAsync(Profile requesterProfile, Profile requestedProfile)
        {
            if (!requesterProfile.IsValid)
                return CommandResult.Failure(requesterProfile.GetNotifications().ToList());
            if (!requestedProfile.IsValid)
                return CommandResult.Failure(requestedProfile.GetNotifications().ToList());

            await _profileRepository.UpdateAsync(requesterProfile);
            await _profileRepository.UpdateAsync(requestedProfile);

            return CommandResult.Ok();
        }
    }
}
