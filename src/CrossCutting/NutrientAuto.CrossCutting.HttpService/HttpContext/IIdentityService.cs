using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace NutrientAuto.CrossCutting.HttpService.HttpContext
{
    public interface IIdentityService
    {
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        ClaimsPrincipal GetUser();
        IReadOnlyList<Claim> GetUserClaims();
    }
}
