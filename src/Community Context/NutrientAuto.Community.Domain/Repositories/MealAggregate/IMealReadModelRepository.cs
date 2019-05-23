using NutrientAuto.Community.Domain.ReadModels.MealAggregate;
using System;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.MealAggregate
{
    public interface IMealReadModelRepository
    {
        Task<MealSummaryReadModel> GetMealSummaryAsync(Guid id);
    }
}
