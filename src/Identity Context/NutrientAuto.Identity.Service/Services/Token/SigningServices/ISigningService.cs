using Microsoft.IdentityModel.Tokens;

namespace NutrientAuto.Identity.Domain.Services.Token.SigningServices
{
    public interface ISigningService
    {
        SecurityKey SecurityKey { get; }
        SigningCredentials GetSigningCredentials();
    }
}
