using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Shared.PipelineBehaviours
{
    public class CommandValidatorPipelineBehaviour<TCommand, TCommandResult> : IPipelineBehavior<TCommand, TCommandResult>
        where TCommand : Command
        where TCommandResult : CommandResult
    {
        private readonly ILogger<CommandValidatorPipelineBehaviour<TCommand, TCommandResult>> _logger;

        public CommandValidatorPipelineBehaviour(ILogger<CommandValidatorPipelineBehaviour<TCommand, TCommandResult>> logger)
        {
            _logger = logger;
        }

        public async Task<TCommandResult> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<TCommandResult> next)
        {
            _logger.LogInformation("Comando recebido: {command}", request);

            if (!request.Validate())
            {
                List<DomainNotification> commandErrors = request.ValidationResult.Errors
                    .Select(failure => new DomainNotification(failure.ErrorCode, failure.ErrorMessage))
                    .ToList();

                _logger.LogWarning("Validação do {comando} falhou: {validationErrors}", request, request.ValidationResult.Errors);
                return (TCommandResult)CommandResult.Failure(commandErrors);
            }

            _logger.LogInformation("Comando {command} validado com sucesso.", request);
            return await next();
        }
    }
}
