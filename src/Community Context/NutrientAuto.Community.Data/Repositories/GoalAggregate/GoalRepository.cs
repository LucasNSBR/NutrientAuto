using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.GoalAggregate;
using NutrientAuto.Community.Domain.Repositories.GoalAggregate;

namespace NutrientAuto.Community.Data.Repositories.GoalAggregate
{
    public class GoalRepository : BaseProfileEntityRepository<Goal>, IGoalRepository
    {
        public GoalRepository(CommunityDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
