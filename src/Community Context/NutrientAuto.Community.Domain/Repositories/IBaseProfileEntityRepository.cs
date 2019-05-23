using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories
{
    public interface IBaseProfileEntityRepository<TEntity>
        where TEntity : Entity<TEntity>, IProfileEntity
    {
        Task<List<TEntity>> GetAllByProfileIdAsync(Guid profileId);
        Task<TEntity> GetByIdAsync(Guid id);
        Task RegisterAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
