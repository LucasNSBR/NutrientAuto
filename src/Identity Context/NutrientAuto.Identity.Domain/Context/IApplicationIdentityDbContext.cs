using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;

namespace NutrientAuto.Identity.Domain.Context
{
    public interface IApplicationIdentityDbContext : IDbContext<IApplicationIdentityDbContext>
    {
    }
}
