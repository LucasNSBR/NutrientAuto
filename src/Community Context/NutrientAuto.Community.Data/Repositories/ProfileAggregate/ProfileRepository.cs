using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.ProfileAggregate
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CommunityDbContext _dbContext;

        public ProfileRepository(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Profile>> GetAllAsync()
        {
            return _dbContext
                .Profiles
                .Include(p => p.Friends)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Profile> GetByIdAsync(Guid id)
        {
            return _dbContext
                .Profiles
                .Include(p => p.Friends)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public Task RegisterAsync(Profile profile)
        {
            return _dbContext
                .AddAsync(profile);
        }

        public Task UpdateAsync(Profile profile)
        {
            return Task.FromResult(_dbContext
                .Update(profile));
        }

        public Task RemoveAsync(Profile profile)
        {
            return Task.FromResult(_dbContext
                .Remove(profile));
        }
    }
}
