using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.Repositories;
using NutrientAuto.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories
{
    public abstract class BaseProfileEntityRepository<TEntity> : IBaseProfileEntityRepository<TEntity>
        where TEntity : Entity<TEntity>, IProfileEntity
    {
        protected readonly CommunityDbContext _dbContext;

        protected BaseProfileEntityRepository(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual Task<List<TEntity>> GetAllByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .Set<TEntity>()
                .Where(g => g.ProfileId == profileId)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual Task<TEntity> GetByIdAsync(Guid id)
        {
            return _dbContext
                .Set<TEntity>()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public virtual Task RegisterAsync(TEntity entity)
        {
            return _dbContext
                .AddAsync(entity);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            return Task.FromResult(_dbContext
                .Update(entity));
        }

        public virtual Task RemoveAsync(TEntity entity)
        {
            return Task.FromResult(_dbContext
                .Remove(entity));
        }
    }
}
