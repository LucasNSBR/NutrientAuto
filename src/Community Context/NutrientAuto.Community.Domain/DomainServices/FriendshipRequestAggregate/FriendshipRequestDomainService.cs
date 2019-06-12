using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate;
using NutrientAuto.Shared.Commands;
using System;
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
                return CommandResult.Failure("Falha ao criar nova solicitação", "Você já tem uma solicitação de amizade ativa para esse usuário.");

            friendshipRequest.Dump();

            await _friendshipRequestRepository.UpdateAsync(friendshipRequest);
            return CommandResult.Ok();
        }
    }
}
