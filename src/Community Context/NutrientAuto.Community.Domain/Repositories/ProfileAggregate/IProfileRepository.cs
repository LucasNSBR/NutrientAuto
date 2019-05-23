using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.ProfileAggregate
{
    public interface IProfileRepository
    {
        Task<List<Profile>> GetAllAsync();
        Task<Profile> GetByIdAsync(Guid id);
        Task RegisterAsync(Profile profile);
        Task UpdateAsync(Profile profile);
        Task RemoveAsync(Profile profile);
    }
}
