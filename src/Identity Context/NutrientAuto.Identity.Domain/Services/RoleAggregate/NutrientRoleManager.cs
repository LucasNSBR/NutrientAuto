using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NutrientAuto.Identity.Domain.Aggregates.RoleAggregate;
using System.Collections.Generic;

namespace NutrientAuto.Identity.Domain.Services.RoleAggregate
{
    public class NutrientRoleManager : RoleManager<NutrientIdentityRole>
    {
        public NutrientRoleManager(IRoleStore<NutrientIdentityRole> store, IEnumerable<IRoleValidator<NutrientIdentityRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<NutrientIdentityRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
