using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.DietAggregate;
using NutrientAuto.Community.Domain.Repositories.DietAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.DietAggregate
{
    public class DietRepository : BaseProfileEntityRepository<Diet>, IDietRepository
    {
        public DietRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Task<List<Diet>> GetAllByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .Diets
                .Where(g => g.ProfileId == profileId)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<Diet> GetByIdAsync(Guid id)
        {
            return _dbContext
                .Diets
                .Include(d => d.DietMeals)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
