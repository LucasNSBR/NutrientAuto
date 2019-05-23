using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.Repositories.PostAggregate;

namespace NutrientAuto.Community.Data.Repositories.PostAggregate
{
    public class PostRepository : BaseProfileEntityRepository<Post>, IPostRepository
    {
        public PostRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
