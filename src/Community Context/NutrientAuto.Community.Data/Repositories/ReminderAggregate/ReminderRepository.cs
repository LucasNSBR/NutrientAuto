using NutrientAuto.Community.Data.Context;
using NutrientAuto.Community.Domain.Aggregates.ReminderAggregate;

namespace NutrientAuto.Community.Data.Repositories.ReminderAggregate
{
    public class ReminderRepository : BaseProfileEntityRepository<Reminder>
    {
        public ReminderRepository(CommunityDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
