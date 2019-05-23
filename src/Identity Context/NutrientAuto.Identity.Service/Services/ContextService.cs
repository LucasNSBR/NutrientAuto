using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.ServiceBus.IntegrationBus;
using NutrientAuto.Identity.Domain.Aggregates.UserAggregate;
using NutrientAuto.Identity.Domain.Models.Services.UserAggregate;
using NutrientAuto.Shared.Notifications;
using NutrientAuto.Shared.Service.Services;

namespace NutrientAuto.Identity.Service.Services
{
    public abstract class ContextService : BaseService
    {
        protected readonly NutrientUserManager _userManager;

        protected ContextService(NutrientUserManager userManager, IDomainNotificationHandler domainNotificationHandler, IIntegrationServiceBus integrationBus, ILogger logger)
            : base(domainNotificationHandler, integrationBus, logger)
        {
            _userManager = userManager;
        }

        protected bool NotifyIdentityErrors(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    AddNotification(error.Code, error.Description);
                }

                _logger.LogError("Operação com identity falhou: {identityErrors}", result.Errors);
                return true;
            }

            _logger.LogError("Operação com identity completada com sucesso.");
            return false;
        }

        protected bool NotifyNullUser(NutrientIdentityUser user, string id)
        {
            if (user == null)
            {
                _logger.LogError("Usuário do Identity {id} não foi encontrado.", id);

                AddNotification("Usuário inválido", "Nenhum usuário com esse e-mail foi encontrado no banco de dados.");
                return true;
            }

            return false;
        }
    }
}
