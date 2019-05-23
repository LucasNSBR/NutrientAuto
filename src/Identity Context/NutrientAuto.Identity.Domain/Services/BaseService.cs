using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.ServiceBus.IntegrationBus;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.IntegrationEvents.Events;
using NutrientAuto.Shared.Notifications;
using System.Threading.Tasks;

namespace NutrientAuto.Shared.Service.Services
{
    public abstract class BaseService
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        private readonly IIntegrationServiceBus _integrationBus;
        protected readonly ILogger _logger;

        protected BaseService(IDomainNotificationHandler domainNotificationHandler, IIntegrationServiceBus integrationBus, ILogger logger)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _integrationBus = integrationBus;
            _logger = logger;
        }

        protected bool NotifyCommandErrors(Command command)
        {
            _logger.LogInformation("Comando recebido: {command}", command);
            
            if (!command.Validate())
            {
                foreach (ValidationFailure failure in command.ValidationResult.Errors)
                {
                    AddNotification(failure.ErrorCode, failure.ErrorMessage);
                }

                _logger.LogWarning("Validação do {comando} falhou: {validationErrors}", command, command.ValidationResult.Errors);
                return true;
            }

            _logger.LogInformation("Comando {command} validado com sucesso.", command);
            return false;
        }

        protected void AddNotification(string title, string description)
        {
            _logger.LogWarning("Notificação adicionada: {notification}", new { title, description });

            _domainNotificationHandler.AddNotification(title, description);
        }

        protected bool AddNotifications(IDomainNotifier notifier)
        {
            _logger.LogInformation("Validador de domínio recebido: {notifier}", notifier);

            if (!notifier.IsValid)
            {
                foreach (DomainNotification notification in notifier.GetNotifications())
                {
                    AddNotification(notification.Title, notification.Description);
                }

                _logger.LogWarning("Validação de domínio {notifier} falhou: {notifications}", notifier.GetNotificationsAsDictionary());
                return true;
            }

            _logger.LogInformation("Validador de domínio {notifier} feita com sucesso.", notifier);
            return false;
        }

        protected Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent) where TIntegrationEvent : IntegrationEvent
        {
            _logger.LogInformation("Evento de Integração publicado: {integrationEvent}", integrationEvent);

            return _integrationBus.PublishAsync(integrationEvent);
        }
    }
}
