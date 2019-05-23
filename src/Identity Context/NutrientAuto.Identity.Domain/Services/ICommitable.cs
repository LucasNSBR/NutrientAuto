using System.Threading.Tasks;

namespace NutrientAuto.Shared.Service.Services
{
    public interface ICommitable
    {
        Task<bool> CommitAsync();
    }
}
