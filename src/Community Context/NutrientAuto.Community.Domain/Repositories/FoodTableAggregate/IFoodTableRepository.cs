using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.FoodTableAggregate
{
    public interface IFoodTableRepository
    {
        Task<List<FoodTable>> GetAllDefaultsAsync();
        Task<List<FoodTable>> GetAllByProfileIdAsync(Guid profileId);
        Task<List<CustomFoodTable>> GetAllCustomsByProfileIdAsync(Guid profileId);
        Task<FoodTable> GetDefaultByIdAsync(Guid id);
        Task<FoodTable> GetByIdAsync(Guid id);
        Task<FoodTable> GetByIdAndProfileIdAsync(Guid id, Guid profileId);
        Task<CustomFoodTable> GetCustomByIdAsync(Guid id, Guid profileId);
        Task RegisterAsync<TFoodTable>(TFoodTable foodTable) where TFoodTable : FoodTable;
        Task UpdateAsync<TFoodTable>(TFoodTable foodTable) where TFoodTable : FoodTable;
        Task RemoveAsync<TFoodTable>(TFoodTable foodTable) where TFoodTable : FoodTable;
    }
}
