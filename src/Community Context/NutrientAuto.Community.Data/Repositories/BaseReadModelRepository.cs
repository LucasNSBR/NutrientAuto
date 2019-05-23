using Dapper;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories
{
    public abstract class BaseReadModelRepository
    {
        protected readonly CommunityDbContext _dbContext;

        protected BaseReadModelRepository(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected Task<IEnumerable<TReadModel>> GetAllAsync<TReadModel>(string sql, object parameters = null)
        {
            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return connection
                    .QueryAsync<TReadModel>(sql, parameters);
            }
        }

        protected Task<TReadModel> GetByIdAsync<TReadModel>(string sql, object parameters = null)
        {
            using (DbConnection connection = _dbContext.Database.GetDbConnection())
            {
                return connection
                    .QueryFirstOrDefaultAsync<TReadModel>(sql, parameters);
            }
        }
    }
}
