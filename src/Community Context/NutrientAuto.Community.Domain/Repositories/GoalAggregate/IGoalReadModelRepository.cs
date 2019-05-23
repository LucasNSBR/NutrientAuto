using NutrientAuto.Community.Domain.ReadModels.GoalAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.GoalAggregate
{
    public interface IGoalReadModelRepository
    {
        Task<IEnumerable<GoalListReadModel>> GetGoalListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20);
        Task<GoalSummaryReadModel> GetGoalSummaryAsync(Guid id);
    }
}
