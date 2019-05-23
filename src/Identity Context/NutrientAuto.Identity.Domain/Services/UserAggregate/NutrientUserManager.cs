using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NutrientAuto.Identity.Domain.Models.Services.UserAggregate
{
    public class NutrientUserManager : UserManager<NutrientIdentityUser>
    {
        public NutrientUserManager(IUserStore<NutrientIdentityUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<NutrientIdentityUser> passwordHasher, IEnumerable<IUserValidator<NutrientIdentityUser>> userValidators, IEnumerable<IPasswordValidator<NutrientIdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<NutrientIdentityUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<IdentityResult> AddOrUpdateDefaultUserClaims(NutrientIdentityUser user)
        {
            IdentityResult emailClaimResult = await AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
            IdentityResult nameClaimResult = await AddClaimAsync(user, new Claim(ClaimTypes.Name, user.Name));
            IdentityResult birthDateClaimResult = await AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString("dd/MM/yyyy")));

            IdentityError[] errors = emailClaimResult.Errors
                .Union(nameClaimResult.Errors)
                .Union(birthDateClaimResult.Errors).ToArray();

            if (errors.Any())
                return IdentityResult.Failed(errors);
            else
                return IdentityResult.Success;
        }
    }
}
