using NutrientAuto.Shared.Commands;
using System;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainServices.FriendshipRequestAggregate
{
    public interface IFriendshipRequestDomainService
    {
        Task<CommandResult> DumpExistingFriendshipRequest(Guid profileId, Guid friendId);
    }
}
