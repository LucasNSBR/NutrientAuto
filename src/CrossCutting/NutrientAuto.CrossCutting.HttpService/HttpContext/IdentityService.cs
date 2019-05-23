using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace NutrientAuto.CrossCutting.HttpService.HttpContext
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public Guid GetUserId()
        {
            if (IsAuthenticated())
            {
                string value = _httpContextAccessor.HttpContext.User.Identity.Name;

                if (Guid.TryParse(value, out Guid id))
                {
                    if (id != Guid.Empty)
                        return id;
                    else
                        throw new InvalidCastException("Failed to parse user id.");
                }
            }

            throw new InvalidOperationException("User not authenticated.");
        }

        public string GetUserEmail()
        {
            if (IsAuthenticated())
            {
                Claim email = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Email);

                if (email != null)
                    return email.Value;
                else
                    throw new InvalidOperationException("Failed to get user email claim.");

            }

            throw new InvalidOperationException("User not authenticated.");
        }

        public IReadOnlyList<Claim> GetUserClaims()
        {
            if (IsAuthenticated())
            {
                return _httpContextAccessor.HttpContext.User.Claims.ToList();
            }

            throw new InvalidOperationException("User not authenticated.");
        }

        public ClaimsPrincipal GetUser()
        {
            if (IsAuthenticated())
            {
                return _httpContextAccessor.HttpContext.User;
            }

            throw new InvalidOperationException("User not authenticated.");
        }
    }
}
