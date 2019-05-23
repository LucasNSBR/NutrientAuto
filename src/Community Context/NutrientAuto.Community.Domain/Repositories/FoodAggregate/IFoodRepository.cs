using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.Repositories.FoodAggregate
{
    public interface IFoodRepository
    {
        Task<List<Food>> GetAllDefaultsAsync();
        Task<List<Food>> GetAllByProfileIdAsync(Guid profileId);
        Task<List<CustomFood>> GetAllCustomsByProfileIdAsync(Guid profileId);
        Task<List<Food>> GetAllByFoodTableIdAsync(Guid foodTableId, Guid profileId);
        Task<Food> GetDefaultByIdAsync(Guid id);
        Task<Food> GetByIdAsync(Guid id);
        Task<Food> GetByIdAndProfileIdAsync(Guid id, Guid profileId);
        Task<CustomFood> GetCustomByIdAsync(Guid id, Guid profileId);
        Task RegisterAsync<TFood>(TFood food) where TFood : Food;
        Task UpdateAsync<TFood>(TFood food) where TFood : Food;
        Task RemoveAsync<TFood>(TFood food) where TFood : Food;
    }
}
