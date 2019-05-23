using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.ReadModels.GoalAggregate;
using NutrientAuto.Community.Domain.Repositories.GoalAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.GoalAggregate
{
    public class GoalReadModelRepository : BaseReadModelRepository, IGoalReadModelRepository
    {
        public GoalReadModelRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<IEnumerable<GoalListReadModel>> GetGoalListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = @"SELECT Id, ProfileId, Title, DateCreated, IsCompleted FROM Goals 
                         WHERE Title LIKE %@titleFilter%
                         ORDER BY DateCreated DESC
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            return GetAllAsync<GoalListReadModel>(sql, new { profileId, titleFilter = titleFilter ?? string.Empty, pageNumber, pageSize });
        }

        public Task<GoalSummaryReadModel> GetGoalSummaryAsync(Guid id)
        {
            string sql = @"SELECT Id, ProfileId, Title, Details, DateCreated, IsCompleted, DateCompleted, AccomplishmentDetails FROM Goals
                         WHERE Id = @id";

            return GetByIdAsync<GoalSummaryReadModel>(sql, new { id });
        }
    }
}
