using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;
using NutrientAuto.Identity.Domain.Services.Token.Factories;
using NutrientAuto.Identity.Service.Services.Account;
using NutrientAuto.Shared.Notifications;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Identity
{
    [Produces("application/json")]
    [Route("api/accounts")]
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IJwtFactory _jwtFactory;

        public AccountsController(IAccountService accountService, IJwtFactory jwtFactory, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<AccountsController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _accountService = accountService;
            _jwtFactory = jwtFactory;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterUserCommand command)
        {
            NutrientIdentityUser user = await _accountService.RegisterAsync(command);

            if (user != null)
            {
                return CreateResponse(new
                {
                    id = user.Id,
                    email = user.Email
                });
            }

            return CreateResponse();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginUserCommand command)
        {
            ClaimsIdentity identity = await _accountService.LoginAsync(command);

            if (identity != null)
            {
                string token = _jwtFactory.WriteToken(identity);
                return CreateResponse(new
                {
                    id = identity.Name,
                    email = command.Email,
                    token 
                });
            }

            return CreateResponse();
        }

        [HttpPost]
        [Route("send-confirmation-email")]
        public async Task<IActionResult> GenerateAccountConfirmationEmailAsync([FromBody]SendAccountConfirmationEmailCommand command)
        {
            await _accountService.GenerateAccountConfirmationEmailAsync(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody]ConfirmUserEmailCommand command)
        {
            await _accountService.ConfirmEmailAsync(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody]ForgotUserPasswordCommand command)
        {
            await _accountService.ForgotPasswordAsync(command);

            return CreateResponse();
        }

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody]ResetUserPasswordCommand command)
        {
            await _accountService.ResetPasswordAsync(command);

            return CreateResponse();
        }

        [HttpPost]
        [Authorize]
        [Route("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody]ChangeUserPasswordCommand command)
        {
            await _accountService.ChangePasswordAsync(command);

            return CreateResponse();
        }
    }
}