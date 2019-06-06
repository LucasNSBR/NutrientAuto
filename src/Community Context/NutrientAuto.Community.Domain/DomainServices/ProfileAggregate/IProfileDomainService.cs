using NutrientAuto.Shared.Commands;
using System;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainServices.ProfileAggregate
{
    public interface IProfileDomainService
    {
        Task<ProfileAccessResult> CanAccessProfileData(Guid requesterId, Guid requestedId);
        Task<CommandResult> MakeFriends(Guid requesterId, Guid requestedId);
        Task<CommandResult> EndFriendship(Guid requesterId, Guid requestedId);
    }
}
