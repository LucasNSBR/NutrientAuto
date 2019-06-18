using NutrientAuto.Community.Domain.ReadModels.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.ProfileAggregate
{
    public interface IProfileReadModelRepository
    {
        Task<IEnumerable<ProfileListReadModel>> GetProfileListAsync(string nameFilter = null, int pageNumber = 1, int pageSize = 20);
        Task<ProfileSummaryReadModel> GetProfileSummaryAsync(Guid id);
        Task<ProfileOverviewReadModel> GetProfileOverviewAsync(Guid id);
        Task<ProfileSettingsReadModel> GetProfileSettingsAsync(Guid id);
        Task<IEnumerable<ProfileFriendReadModel>> GetProfileFriendsAsync(Guid id, string nameFilter = null); 
    }
}
