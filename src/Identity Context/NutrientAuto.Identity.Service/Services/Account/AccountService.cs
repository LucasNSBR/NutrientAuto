using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.EmailService.Models;
using NutrientAuto.CrossCutting.EmailService.Services.Dispatcher;
using NutrientAuto.CrossCutting.ServiceBus.IntegrationBus;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using NutrientAuto.Identity.Domain.Commands.UserAggregate;
using NutrientAuto.Identity.Domain.Models.Services.UserAggregate;
using NutrientAuto.Shared.IntegrationEvents.Events.Identity;
using NutrientAuto.Shared.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NutrientAuto.Identity.Service.Services.Account
{
    public class AccountService : ContextService, IAccountService
    {
        private readonly NutrientSignInManager _signInManager;
        private readonly IEmailDispatcher _emailDispatcher;
        private readonly IMediator _mediator;

        public AccountService(NutrientUserManager userManager, NutrientSignInManager signInManager, IEmailDispatcher emailDispatcher, IMediator mediator, IDomainNotificationHandler domainNotificationHandler, IIntegrationServiceBus integrationBus, ILogger<AccountService> logger)
            : base(userManager, domainNotificationHandler, integrationBus, logger)
        {
            _signInManager = signInManager;
            _emailDispatcher = emailDispatcher;
            _mediator = mediator;
        }

        public async Task<NutrientIdentityUser> RegisterAsync(RegisterUserCommand command)
        {
            if (NotifyCommandErrors(command))
                return null;

            NutrientIdentityUser user = new NutrientIdentityUser(command.Name, command.Email, command.BirthDate);

            IdentityResult result = await _userManager.CreateAsync(user, command.Password);
            if (result.Succeeded)
            {
                await SendAccountConfirmationEmailAsync(user);
                await PublishAsync(new UserRegisteredIntegrationEvent(user.Id,
                    command.Genre,
                    user.Name,
                    user.UserName,
                    user.Email,
                    user.BirthDate));

                return user;
            }

            NotifyIdentityErrors(result);
            return null;
        }

        public async Task<ClaimsIdentity> LoginAsync(LoginUserCommand command)
        {
            if (NotifyCommandErrors(command))
                return null;

            NutrientIdentityUser user = await _userManager.FindByEmailAsync(command.Email);
            if (NotifyNullUser(user, command.Email))
                return null;

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
            if (result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                IEnumerable<string> roles = await _userManager.GetRolesAsync(user);

                List<Claim> userPermissions = new List<Claim>();
                userPermissions.AddRange(claims);
                userPermissions.AddRange(roles.Select(value => new Claim(ClaimTypes.Role, value)));

                GenericIdentity genericIdentity = new GenericIdentity(user.Id.ToString());
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(genericIdentity, userPermissions);

                _logger.LogInformation("Autenticação bem sucedida para o usuário {email}.", user.Email);

                return claimsIdentity;
            }

            _logger.LogWarning("Autenticação do usuário {email} falhou.", command.Email);

            AddNotification("Usuário inválido", "O usuário e/ou senha estão incorretos.");
            return null;
        }

        public async Task GenerateAccountConfirmationEmailAsync(SendAccountConfirmationEmailCommand command)
        {
            if (NotifyCommandErrors(command))
                return;

            NutrientIdentityUser user = await _userManager.FindByEmailAsync(command.Email);
            if (!NotifyNullUser(user, command.Email))
                await SendAccountConfirmationEmailAsync(user);
        }

        private async Task SendAccountConfirmationEmailAsync(NutrientIdentityUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            EmailTemplate emailTemplate = new EmailTemplate()
                .SetTo(user.Email)
                .SetTemplate(TemplateTypes.EmailConfirmationTemplateId)
                .AddSubstitution("-token-", token);

            _logger.LogInformation("Email {template} sendo enviado para o usuário {email}.", emailTemplate, user.Email);

            await _emailDispatcher.SendEmailAsync(emailTemplate);
        }

        public async Task ConfirmEmailAsync(ConfirmUserEmailCommand command)
        {
            if (NotifyCommandErrors(command))
                return;

            NutrientIdentityUser user = await _userManager.FindByEmailAsync(command.Email);
            if (NotifyNullUser(user, command.Email))
                return;

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, command.ConfirmationToken);
            if (!result.Succeeded)
                NotifyIdentityErrors(result);
            else
            {
                IdentityResult claimSuccess = await _userManager.AddClaimAsync(user, new Claim("EmailConfirmed", "true"));
                if (claimSuccess.Succeeded)
                    await PublishAsync(new UserConfirmedEmailIntegrationEvent(user.Id, user.Email));
                else
                {
                    _logger.LogError("Falha ao adicionar claim de confirmação de e-mail para usuário {email}. Motivo: {errors}.", user.Email, claimSuccess.Errors);

                    AddNotification("Falha nas claims", "Ocorreu uma falha ao registrar ou atualizar as claims padrões desse usuário.");
                }
            }
        }

        public async Task ForgotPasswordAsync(ForgotUserPasswordCommand command)
        {
            if (NotifyCommandErrors(command))
                return;

            NutrientIdentityUser user = await _userManager.FindByEmailAsync(command.Email);
            if (!NotifyNullUser(user, command.Email))
                await SendAccountPasswordResetEmailAsync(user);
        }

        private async Task SendAccountPasswordResetEmailAsync(NutrientIdentityUser user)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            EmailTemplate emailTemplate = new EmailTemplate()
                .SetTo(user.Email)
                .SetTemplate(TemplateTypes.EmailConfirmationTemplateId)
                .AddSubstitution("-token-", token);

            _logger.LogInformation("Email {template} sendo enviado para o usuário {email}.", emailTemplate, user.Email);

            await _emailDispatcher.SendEmailAsync(emailTemplate);
        }

        public async Task ResetPasswordAsync(ResetUserPasswordCommand command)
        {
            if (NotifyCommandErrors(command))
                return;

            NutrientIdentityUser user = await _userManager.FindByEmailAsync(command.Email);
            if (NotifyNullUser(user, command.Email))
                return;

            IdentityResult result = await _userManager.ResetPasswordAsync(user, command.ResetToken, command.NewPassword);
            if (!result.Succeeded)
                NotifyIdentityErrors(result);
        }

        public async Task ChangePasswordAsync(ChangeUserPasswordCommand command)
        {
            if (NotifyCommandErrors(command))
                return;

            NutrientIdentityUser user = await _userManager.FindByIdAsync(command.UserId.ToString());
            if (NotifyNullUser(user, command.UserId.ToString()))
                return;

            IdentityResult result = await _userManager.ChangePasswordAsync(user, command.Password, command.NewPassword);
            if (!result.Succeeded)
                NotifyIdentityErrors(result);
        }
    }
}
