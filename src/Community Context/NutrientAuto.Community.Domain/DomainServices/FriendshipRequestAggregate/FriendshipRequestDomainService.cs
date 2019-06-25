using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate;
using NutrientAuto.Shared.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainServices.FriendshipRequestAggregate
{
    public class FriendshipRequestDomainService : IFriendshipRequestDomainService
    {
        private readonly IFriendshipRequestRepository _friendshipRequestRepository;

        public FriendshipRequestDomainService(IFriendshipRequestRepository friendshipRequestRepository)
        {
            _friendshipRequestRepository = friendshipRequestRepository;
        }

        public async Task<CommandResult> DumpExistingFriendshipRequest(Guid profileId, Guid friendId)
        {
            FriendshipRequest friendshipRequest = await _friendshipRequestRepository.GetActiveByCompositeIdAsync(profileId, friendId);
            if (friendshipRequest == null)
                return CommandResult.Failure("Falha ao baixar solicitação existente", "A solicitação de amizade não foi encontrada no banco de dados.");

            friendshipRequest.Dump();
            if (!friendshipRequest.IsValid)
                return CommandResult.Failure(friendshipRequest.GetNotifications().ToList());

            await _friendshipRequestRepository.UpdateAsync(friendshipRequest);
            return CommandResult.Ok();
        }
    }
}
