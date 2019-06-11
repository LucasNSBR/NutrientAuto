using Microsoft.EntityFrameworkCore;
using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.CommentAggregate;
using NutrientAuto.Community.Domain.Repositories.CommentAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Data.Repositories.CommentAggregate
{
    public class CommentRepository : BaseProfileEntityRepository<Comment>, ICommentRepository
    {
        public CommentRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }

        public override Task<List<Comment>> GetAllByProfileIdAsync(Guid profileId)
        {
            return _dbContext
                .Comments
                .Include(c => c.Replies)
                .AsNoTracking()
                .ToListAsync();
        }

        public override Task<Comment> GetByIdAsync(Guid id)
        {
            return _dbContext
                .Comments
                .Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
