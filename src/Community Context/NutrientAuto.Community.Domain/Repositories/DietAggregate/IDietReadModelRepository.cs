using NutrientAuto.Community.Domain.ReadModels.DietAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.DietAggregate
{
    public interface IDietReadModelRepository
    {
        Task<IEnumerable<DietListReadModel>> GetDietListAsync(Guid profileId, string nameFilter = null, int pageNumber = 1, int pageSize = 20);
        Task<DietSummaryReadModel> GetDietSummaryAsync(Guid id);
    }
}
