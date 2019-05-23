using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;

namespace NutrientAuto.Community.Domain.Context
{
    public interface ICommunityDbContext : IDbContext<ICommunityDbContext>
    {
    }
}
