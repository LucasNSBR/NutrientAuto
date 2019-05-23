using NutrientAuto.Community.Domain.Aggregates.MealAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.MealAggregate
{
    public interface IMealRepository : IBaseProfileEntityRepository<Meal>
    {
        Task<List<Meal>> GetByDietIdAsync(Guid dietId);
    }
}
