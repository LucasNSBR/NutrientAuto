using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.Notifications;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly IDomainNotificationHandler _domainNotificationHandler;
        protected readonly IMediator _mediator;
        protected readonly ILogger _logger;

        protected BaseController(IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger logger)
        {
            _domainNotificationHandler = domainNotificationHandler;
            _mediator = mediator;
            _logger = logger;
        }

        protected void AddNotifications(List<DomainNotification> notifications)
        {
            _domainNotificationHandler.AddNotifications(notifications);
        }
        
        protected async Task<IActionResult> CreateCommandResponse<TCommand>(TCommand command)
            where TCommand : IRequest<CommandResult>
        {
            CommandResult result = await _mediator.Send(command);

            if (!result.Success)
            {
                foreach (DomainNotification notification in result.Notifications)
                {
                    _domainNotificationHandler.AddNotification(notification.Title, notification.Description);
                }
            }

            return CreateResponse();
        }

        protected IActionResult CreateResponse(object result = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (_domainNotificationHandler.HasNotifications())
            {
                _logger.LogWarning("Retornando resposta do Controller: BadRequest devido à {notifications}.", _domainNotificationHandler.GetNotifications());

                return BadRequest(new
                {
                    success = false,
                    result = "Ocorreu um erro ao retornar os resultados.",
                    data = _domainNotificationHandler
                            .GetNotifications()
                });
            }

            _logger.LogInformation("Retornando resposta do Controller: Operação concluída com sucesso, dados retornados: {data}.", result);

            return StatusCode((int)statusCode, new
            {
                success = true,
                result = "A operação foi concluída com sucesso.",
                data = result
            });
        }

        protected IActionResult CreateErrorResponse()
        {
            return CreateResponse(statusCode: HttpStatusCode.BadRequest);
        }

        protected IActionResult CreateErrorResponse(string title, string description)
        {
            _domainNotificationHandler.AddNotification(title, description);

            return CreateResponse();
        }

        protected IActionResult CreateErrorResponse(ValidationResult validationResult)
        {
            foreach (ValidationFailure failure in validationResult.Errors)
                _domainNotificationHandler.AddNotification(failure.ErrorCode, failure.ErrorMessage);

            return CreateResponse();
        }
    }
}