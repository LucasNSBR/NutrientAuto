using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Shared.CommandHandlers;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.Events;
using System;
using System.Threading.Tasks;
using NutrientAuto.CrossCutting.Storage.Services.StorageValidators;
using NutrientAuto.Shared.Notifications;
using System.Collections.Generic;

namespace NutrientAuto.Community.Domain.CommandHandlers
{
    public abstract class ContextCommandHandler : BaseCommandHandler<ICommunityDbContext>
    {
        private readonly IIdentityService _identityService;

        protected ContextCommandHandler(IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger logger)
            : base(mediator, unitOfWork, logger)
        {
            _identityService = identityService;
        }

        protected Guid GetCurrentProfileId()
        {
            return _identityService.GetUserId();
        }

        protected CommandResult FailureDueToProfileNotFound(string title = null, string description = null)
        {
            return FailureDueToEntityNotFound(title ?? "Id do perfil inválido", description ?? "Falha ao buscar perfil no banco de dados.");
        }

        protected CommandResult FailureDueToDietNotFound(string title = null, string description = null)
        {
            return FailureDueToEntityNotFound(title ?? "Dieta não encontrada", description ?? "Falha ao buscar dieta no banco de dados.");
        }

        protected CommandResult FailureDueToCustomFoodNotFound(string title = null, string description = null)
        {
            return FailureDueToEntityNotFound(title ?? "Erro de alimento", description ?? "Nenhum alimento com esse Id foi encontrado no banco de dados.");
        }

        protected CommandResult FailureDueToCustomFoodTableNotFound(string title = null, string description = null)
        {
            return FailureDueToEntityNotFound(title ?? "Erro de categoria", description ?? "Nenhuma categoria com esse Id foi encontrada no banco de dados.");
        }

        protected CommandResult FailureDueToCustomMeasureCategoryNotFound(string title = null, string description = null)
        {
            return FailureDueToEntityNotFound(title ?? "Erro de categoria", description ?? "Nenhuma categoria de medição com esse Id foi encontrada no banco de dados.");
        }

        protected CommandResult FailureDueToPostNotFound(string title = null, string description = null)
        {
            return FailureDueToEntityNotFound(title ?? "Id da Publicação inválido", description ?? "Falha ao buscar Publicação no banco de dados.");
        }

        protected CommandResult FailureDueToFileValidationFailure(StorageValidatorResult storageValidatorResult)
        {
            List<DomainNotification> notifications = new List<DomainNotification>();

            foreach (StorageValidatorError storageValidatorError in storageValidatorResult.Errors)
            {
                notifications.Add(new DomainNotification(storageValidatorError.ErrorCode, storageValidatorError.ErrorMessage));
            }

            return FailureDueTo(notifications);
        }

        protected CommandResult FailureDueToUploadFailure(string title = null, string description = null)
        {
            return FailureDueTo(title ?? "Falha no upload", description ?? "Ocorreu uma falha ao fazer upload da imagem para o servidor de armazenamento.");
        }

        protected async Task<CommandResult> CommitAndPublishAsync<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : DomainEvent
        {
            bool success = await CommitAsync();
            if (success)
            {
                if (domainEvent != null)
                    await PublishDomainAsync(domainEvent);

                return CommandResult.Ok();
            }

            return FailureDueToPersistenceError();
        }
    }
}
