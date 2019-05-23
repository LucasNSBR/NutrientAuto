using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.EmailService.Models;
using NutrientAuto.CrossCutting.EmailService.Services.Dispatcher;
using NutrientAuto.CrossCutting.ServiceBus.IntegrationBus;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using NutrientAuto.Identity.Domain.Models.Services.UserAggregate;
using NutrientAuto.Shared.Notifications;
using NutrientAuto.Shared.ValueObjects;
using NutrientAuto.Shared.ValueObjectValidators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.Identity.Service.Services.User
{
    public class UsersService : ContextService, IUsersService
    {
        private readonly IEmailDispatcher _emailDispatcher;

        public UsersService(IEmailDispatcher emailDispatcher, NutrientUserManager userManager, IDomainNotificationHandler domainNotificationHandler, IIntegrationServiceBus integrationBus, ILogger<UsersService> logger)
            : base(userManager, domainNotificationHandler, integrationBus, logger)
        {
            _emailDispatcher = emailDispatcher;
        }

        public Task<List<NutrientIdentityUser>> GetAllAsync()
        {
            return _userManager.Users.ToListAsync();
        }

        public async Task<NutrientIdentityUser> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<NutrientIdentityUser> GetByIdAsync(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task InviteUserAsync(Guid inviterId, string invitedEmail)
        {
            ValidationResult result = new EmailAddressValidator()
                .Validate(new EmailAddress(invitedEmail));

            if (!result.IsValid)
            {
                foreach (ValidationFailure failure in result.Errors)
                {
                    AddNotification(failure.ErrorCode, failure.ErrorMessage);
                }

                return;
            }
             
            NutrientIdentityUser inviter = await _userManager.FindByIdAsync(inviterId.ToString());
            NutrientIdentityUser invited = await _userManager.FindByEmailAsync(invitedEmail);
            if (invited != null)
                AddNotification("Usuário já existe", "O usuário já está cadastrado na rede Nutrient.");
            else
            {
                EmailTemplate emailTemplate = new EmailTemplate()
                    .SetTo(invitedEmail)
                    .SetTemplate(TemplateTypes.UserInvitationTemplateId)
                    .AddSubstitution("-inviterName-", inviter.Name)
                    .AddSubstitution("-inviterEmail-", inviter.Email)
                    .AddSubstitution("-invitedEmail-", invitedEmail);

                await _emailDispatcher.SendEmailAsync(emailTemplate);
            }
        }
    }
}
