using NutrientAuto.Community.Domain.ReadModels.FriendshipRequestAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate
{
    public interface IFriendshipRequestReadModelRepository
    {
        Task<IEnumerable<FriendshipRequestListReadModel>> GetFriendshipRequestList(Guid requestedId, string nameFilter = null, int pageNumber = 1, int pageSize = 20);
        Task<IEnumerable<FriendshipRequestSentListReadModel>> GetFriendshipRequestSentList(Guid requesterId, string nameFilter = null, int pageNumber = 1, int pageSize = 20);
    }
}
