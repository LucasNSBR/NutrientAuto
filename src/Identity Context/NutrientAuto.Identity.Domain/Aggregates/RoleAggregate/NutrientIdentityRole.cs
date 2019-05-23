using Microsoft.AspNetCore.Identity;
using NutrientAuto.Shared.Entities;
using System;

namespace NutrientAuto.Identity.Domain.Aggregates.RoleAggregate
{
    public class NutrientIdentityRole : IdentityRole<Guid>, IAggregateRoot
    {
        protected NutrientIdentityRole()
        {
        }

        public NutrientIdentityRole(string name)
        {
            Name = name;
        }
    }
}
