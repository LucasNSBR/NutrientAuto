using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;

namespace NutrientAuto.Community.Data.Repositories.MeasureAggregate
{
    public class MeasureRepository : BaseProfileEntityRepository<Measure>, IMeasureRepository
    {
        public MeasureRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Task<List<Measure>> GetAllByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .Measures
                .Include(m => m.MeasureLines)
                .ThenInclude(ml => ml.MeasureCategory)
                .Where(g => g.ProfileId == profileId)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<Measure> GetByIdAsync(Guid id)
        {
            return _dbContext
                .Measures
                .AsNoTracking()
                .Include(m => m.MeasureLines)
                .ThenInclude(ml => ml.MeasureCategory)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
