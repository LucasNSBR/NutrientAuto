using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.UnitOfwork.Abstractions
{
    public interface IDbContext<TDbContext> 
            where TDbContext : IDbContext<TDbContext>
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
