using NutrientAuto.CrossCutting.UnitOfWork;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.UnitOfwork.Abstractions
{
    public interface IUnitOfWork<TDbContext> 
        where TDbContext : IDbContext<TDbContext>
    {
        Task<CommitResult> CommitAsync();
        void Rollback();
    }
}