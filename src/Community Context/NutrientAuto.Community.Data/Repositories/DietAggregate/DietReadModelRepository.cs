using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.ReadModels.DietAggregate;
using NutrientAuto.Community.Domain.Repositories.DietAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.DietAggregate
{
    public class DietReadModelRepository : BaseReadModelRepository, IDietReadModelRepository
    {
        public DietReadModelRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<IEnumerable<DietListReadModel>> GetDietListAsync(Guid profileId, string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            string sql = "BIG SQL STATEMENT GOES HERE";

            return GetAllAsync<DietListReadModel>(sql, new { profileId, nameFilter = nameFilter ?? string.Empty, pageNumber, pageSize });
        }

        public Task<DietSummaryReadModel> GetDietSummaryAsync(Guid id)
        {
            string sql = "BIG SQL STATEMENT GOES HERE";

            return GetByIdAsync<DietSummaryReadModel>(sql, new { id });
        }
    }
}
