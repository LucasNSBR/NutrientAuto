using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.FriendshipRequestAggregate
{
    public class FriendshipRequestRepository : IFriendshipRequestRepository
    {
        private readonly CommunityDbContext _dbContext;

        public FriendshipRequestRepository(CommunityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<FriendshipRequest>> GetAllByRequesterIdAsync(Guid requesterId)
        {
            return _dbContext
                .FriendshipRequests
                .AsNoTracking()
                .Where(fr => fr.RequesterId == requesterId)
                .ToListAsync();
        }

        public Task<List<FriendshipRequest>> GetAllByRequestedIdAsync(Guid requestedId)
        {
            return _dbContext
                .FriendshipRequests
                .AsNoTracking()
                .Where(fr => fr.RequestedId == requestedId)
                .ToListAsync();
        }

        public Task<FriendshipRequest> GetByIdAsync(Guid id)
        {
            return _dbContext
                .FriendshipRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(fr => fr.Id == id);
        }

        public Task<FriendshipRequest> GetByCompositeIdAsync(Guid requesterId, Guid requestedId)
        {
            return _dbContext
                .FriendshipRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(fr => fr.RequesterId == requesterId && fr.RequestedId == requestedId);
        }

        public Task RegisterAsync(FriendshipRequest friendshipRequest)
        {
            return _dbContext
                .AddAsync(friendshipRequest);
        }

        public Task UpdateAsync(FriendshipRequest friendshipRequest)
        {
            return Task.FromResult(_dbContext
                .Update(friendshipRequest));
        }

        public Task RemoveAsync(FriendshipRequest friendshipRequest)
        {
            return Task.FromResult(_dbContext
                .Remove(friendshipRequest));
        }
    }
}
