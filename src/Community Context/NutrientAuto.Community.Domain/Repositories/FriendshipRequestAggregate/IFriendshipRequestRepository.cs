using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate
{
    public interface IFriendshipRequestRepository
    {
        Task<List<FriendshipRequest>> GetAllByRequesterIdAsync(Guid requesterId);
        Task<List<FriendshipRequest>> GetAllByRequestedIdAsync(Guid requestedId);
        Task<FriendshipRequest> GetByIdAsync(Guid id);
        Task<FriendshipRequest> GetByCompositeIdAsync(Guid requesterId, Guid requestedId);
        Task RegisterAsync(FriendshipRequest friendshipRequest);
        Task UpdateAsync(FriendshipRequest friendshipRequest);
        Task RemoveAsync(FriendshipRequest friendshipRequest);
    }
}
