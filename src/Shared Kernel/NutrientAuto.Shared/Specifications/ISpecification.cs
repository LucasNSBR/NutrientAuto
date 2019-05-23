using NutrientAuto.Shared.Entities;
using System;
using System.Linq.Expressions;

namespace NutrientAuto.Shared.Specifications
{
    public interface ISpecification<T> where T : Entity<T>
    {
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> ToExpression();
    }
}
