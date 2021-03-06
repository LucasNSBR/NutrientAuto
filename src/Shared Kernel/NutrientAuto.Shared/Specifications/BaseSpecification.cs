﻿using NutrientAuto.Shared.Entities;
using System;
using System.Linq.Expressions;

namespace NutrientAuto.Shared.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T> where T : Entity<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            return ToExpression()
                .Compile()
                .Invoke(entity);
        }

        public Func<T, bool> Compile()
        {
            return ToExpression().Compile();
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
