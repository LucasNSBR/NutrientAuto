using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.ReminderAggregate;
using NutrientAuto.Community.Domain.Commands.ReminderAggregate;
using NutrientAuto.Community.Domain.Repositories.ReminderAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Authorize("ActiveProfile")]
    [Produces("application/json")]
    [Route("api/reminders")]
    public class RemindersController : BaseController
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly Guid _currentProfileId;

        public RemindersController(IReminderRepository reminderRepository, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<RemindersController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _reminderRepository = reminderRepository;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<Reminder>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllByProfileIdAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            List<Reminder> reminders = await _reminderRepository.GetAllByProfileIdAsync(_currentProfileId);

            return CreateResponse(reminders);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(Reminder), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Reminder reminder = await _reminderRepository.GetByIdAsync(id);
            if (reminder.ProfileId != _currentProfileId)
                return CreateResponse();

            return CreateResponse(reminder);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterReminderCommand command)
        {
            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateReminderCommand command)
        {
            command.ReminderId = id;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            RemoveReminderCommand command = new RemoveReminderCommand
            {
                ReminderId = id
            };

            return await CreateCommandResponse(command);
        }
    }
}