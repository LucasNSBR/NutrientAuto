using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NutrientAuto.Identity.Service.Services.Account
{
    public interface IAccountService
    {
        Task<NutrientIdentityUser> RegisterAsync(RegisterUserCommand command);
        Task<ClaimsIdentity> LoginAsync(LoginUserCommand command);
        Task GenerateAccountConfirmationEmailAsync(SendAccountConfirmationEmailCommand command);
        Task ConfirmEmailAsync(ConfirmUserEmailCommand command);
        Task ForgotPasswordAsync(ForgotUserPasswordCommand command);
        Task ResetPasswordAsync(ResetUserPasswordCommand command);
        Task ChangePasswordAsync(ChangeUserPasswordCommand command);
    }
}
