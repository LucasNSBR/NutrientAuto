using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.ReminderAggregate;
using NutrientAuto.Community.Domain.Commands.ReminderAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.ReminderAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.ReminderAggregate
{
    public class ReminderCommandHandler : ContextCommandHandler,
                                          IRequestHandler<RegisterReminderCommand, CommandResult>,
                                          IRequestHandler<UpdateReminderCommand, CommandResult>,
                                          IRequestHandler<RemoveReminderCommand, CommandResult>
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IMapper _mapper;
        private readonly Guid _currentProfileId;

        public ReminderCommandHandler(IReminderRepository reminderRepository, IMapper mapper, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<ReminderCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterReminderCommand request, CancellationToken cancellationToken)
        {
            Reminder reminder = new Reminder(
                _currentProfileId,
                request.Active,
                request.Title,
                request.Details,
                _mapper.Map<Time>(request.TimeOfDay)
                );

            await _reminderRepository.RegisterAsync(reminder);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            Reminder reminder = await _reminderRepository.GetByIdAsync(request.ReminderId);
            if (!FoundValidGoal(reminder))
                return FailureDueToReminderNotFound();

            reminder.Update(
                request.Active,
                request.Title,
                request.Details,
                _mapper.Map<Time>(request.TimeOfDay)
                );

            await _reminderRepository.UpdateAsync(reminder);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveReminderCommand request, CancellationToken cancellationToken)
        {
            Reminder reminder = await _reminderRepository.GetByIdAsync(request.ReminderId);
            if (!FoundValidGoal(reminder))
                return FailureDueToReminderNotFound();

            await _reminderRepository.RemoveAsync(reminder);

            return await CommitAndPublishDefaultAsync();
        }

        private bool FoundValidGoal(Reminder reminder)
        {
            return reminder != null && reminder.ProfileId == _currentProfileId;
        }

        private CommandResult FailureDueToReminderNotFound()
        {
            return FailureDueToEntityNotFound("Id do Lembrete inválido", "Falha ao buscar Lembrete no banco de dados.");
        }
    }
}
