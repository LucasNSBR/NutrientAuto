using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;

namespace NutrientAuto.Identity.Domain.Models.Services.UserAggregate
{
    public class NutrientSignInManager : SignInManager<NutrientIdentityUser>
    {
        public NutrientSignInManager(UserManager<NutrientIdentityUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<NutrientIdentityUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<NutrientIdentityUser>> logger, IAuthenticationSchemeProvider schemes)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }
    }
}
