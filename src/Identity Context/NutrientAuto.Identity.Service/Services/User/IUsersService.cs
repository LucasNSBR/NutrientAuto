using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Identity.Service.Services.User
{
    public interface IUsersService
    {
        Task<List<NutrientIdentityUser>> GetAllAsync();
        Task<NutrientIdentityUser> GetByEmailAsync(string email);
        Task<NutrientIdentityUser> GetByIdAsync(Guid id);
        Task InviteUserAsync(Guid inviterId, string invitedEmail);
    }
}
