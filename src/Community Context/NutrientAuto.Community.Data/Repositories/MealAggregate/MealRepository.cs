using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using NutrientAuto.Community.Domain.Repositories.MealAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.MealAggregate
{
    public class MealRepository : BaseProfileEntityRepository<Meal>, IMealRepository
    {
        public MealRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<List<Meal>> GetByDietIdAsync(Guid dietId)
        {
            return _dbContext
                .Meals
                .Where(m => m.DietId == dietId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
