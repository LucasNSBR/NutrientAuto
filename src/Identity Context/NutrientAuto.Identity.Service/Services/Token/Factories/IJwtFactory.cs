using System.Security.Claims;

namespace NutrientAuto.Identity.Domain.Services.Token.Factories
{
    public interface IJwtFactory
    {
        string WriteToken(ClaimsIdentity claimsIdentity);
    }
}
