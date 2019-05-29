using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using NutrientAuto.Community.Domain.ReadModels.GoalAggregate;
using NutrientAuto.Community.Domain.Repositories.GoalAggregate;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.GoalAggregate
{
    public class GoalReadModelRepository : BaseReadModelRepository, IGoalReadModelRepository
    {
        public GoalReadModelRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<GoalListReadModel>> GetGoalListAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = $@"SELECT Goals.Id, Goals.ProfileId, Goals.Title, Goals.DateCreated, Goals.IsCompleted 
                         FROM Goals
                         WHERE Goals.Title LIKE '%{titleFilter ?? string.Empty}%' AND Goals.ProfileId = @profileId
                         ORDER BY Goals.DateCreated DESC
                         OFFSET (@pageNumber - 1) * @pageSize ROWS
                         FETCH NEXT @pageSize ROWS ONLY";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return await connection
                    .QueryAsync<GoalListReadModel>(sql, new { profileId, titleFilter = titleFilter ?? string.Empty, pageNumber, pageSize });
            }
        }

        public async Task<GoalSummaryReadModel> GetGoalSummaryAsync(Guid id)
        {
            string sql = @"SELECT Goals.Id, Goals.ProfileId, Goals.Title, Goals.Details, Goals.DateCreated, Goals.IsCompleted AS IsCompleted, 
                         Goals.DateCompleted AS DateCompleted, Goals.AccomplishmentDetails AS AccomplishmentDetails  
                         FROM Goals
                         WHERE Id = @id";

            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return (await connection
                    .QueryAsync<GoalSummaryReadModel, GoalStatus, GoalSummaryReadModel>(sql,
                    (goal, status) =>
                    {
                        goal.GoalStatus = status;
                        return goal;
                    },
                    new { id },
                    splitOn: "IsCompleted"))
                    .FirstOrDefault();
            }
        }
    }
}
