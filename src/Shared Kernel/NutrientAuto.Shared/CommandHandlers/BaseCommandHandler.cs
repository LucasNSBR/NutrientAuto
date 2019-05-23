using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.Events;
using NutrientAuto.Shared.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.Shared.CommandHandlers
{
    public abstract class BaseCommandHandler<TContext>
        where TContext : IDbContext<TContext>
    {
        private readonly IMediator _mediator;
        protected readonly ILogger _logger;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        protected BaseCommandHandler(IMediator mediator, IUnitOfWork<TContext> unitOfWork, ILogger logger)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        protected async Task<bool> CommitAsync()
        {
            CommitResult result = await _unitOfWork.CommitAsync();

            if (result.Success)
            {
                _logger.LogInformation("Commit dos dados feito com sucesso.");
                return true;
            }
            else
            {
                _logger.LogError(result.Exception, "Commit dos dados falhou: {message}", result.Exception.Message);
                return false;
            }
        }

        protected async Task<CommandResult> CommitAndPublishDefaultAsync(IEventProducer eventsToPublish = null)
        {
            bool success = await CommitAsync();
            if (success)
            {
                if (eventsToPublish != null)
                    await PublishEntityEventsAsync(eventsToPublish);

                return CommandResult.Ok();
            }

            return FailureDueToPersistenceError();
        }

        protected Task PublishDomainAsync<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : DomainEvent
        {
            _logger.LogInformation("Evento de domínio publicado: {domainEvent}", domainEvent);

            return _mediator.Publish(domainEvent);
        }

        protected async Task PublishEntityEventsAsync(IEventProducer eventProducer)
        {
            foreach (DomainEvent domainEvent in eventProducer.GetEvents())
            {
                _logger.LogInformation("Evento de domínio publicado: {domainEvent}", domainEvent);

                await _mediator.Publish(domainEvent);
            }
        }

        protected CommandResult FailureDueTo(string title, string description)
        {
            return CommandResult.Failure(title, description);
        }

        protected CommandResult FailureDueTo(List<DomainNotification> notifications)
        {
            return CommandResult.Failure(notifications);
        }

        protected CommandResult FailureDueToCommandErrors(Command command)
        {
            if (command.ValidationResult == null)
                command.Validate();

            List<DomainNotification> commandErrors = command.ValidationResult.Errors
                .Select(failure => new DomainNotification(failure.ErrorCode, failure.ErrorMessage))
                .ToList();

            return CommandResult.Failure(commandErrors);
        }

        protected CommandResult FailureDueToEntityNotFound(string title = null, string description = null)
        {
            return CommandResult.Failure(
                title ?? "Id inválido",
                description ?? "Nenhum item com esse Id foi encontrado no banco de dados.");
        }

        protected CommandResult FailureDueToTenantIdInconsistency(string title = null, string description = null)
        {
            return CommandResult.Failure(
                title ?? "Usuário inválido",
                description ?? "Apenas o dono original do registro pode fazer alterações.");
        }

        protected CommandResult FailureDueToEntityStateInconsistency(IDomainNotifier domainNotifier)
        {
            return CommandResult.Failure(domainNotifier
                .GetNotifications()
                .ToList());
        }

        protected CommandResult FailureDueToPersistenceError(string title = null, string description = null)
        {
            return CommandResult.Failure(
                title ?? "Falha de persistência",
                description ?? "Ocorreu uma falha ao persistir os dados dessa operação.");
        }
    }
}
