using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Commands.GoalAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.ReadModels.GoalAggregate;
using NutrientAuto.Community.Domain.Repositories.GoalAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Produces("application/json")]
    //[Authorize(Policy = "ActiveProfile")]
    [Route("api/goals")]
    public class GoalsController : BaseController
    {
        private readonly IGoalReadModelRepository _goalReadModelRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly Guid _currentProfileId;

        public GoalsController(IGoalReadModelRepository goalReadModelRepository, IProfileDomainService profileDomainService, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<GoalsController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _goalReadModelRepository = goalReadModelRepository;
            _profileDomainService = profileDomainService;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("profile/{profileId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<GoalListReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllByProfileIdAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            bool canAccessDiets = await _profileDomainService.CanAccessProfileData(_currentProfileId, profileId);
            if (canAccessDiets)
            {
                IEnumerable<GoalListReadModel> goals = await _goalReadModelRepository.GetGoalListAsync(profileId, titleFilter, pageNumber, pageSize);
                return CreateResponse(goals);
            }

            return Forbid();
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(GoalSummaryReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            GoalSummaryReadModel goal = await _goalReadModelRepository.GetGoalSummaryAsync(id);

            bool canAccessGoal = await _profileDomainService.CanAccessProfileData(_currentProfileId, goal.ProfileId);
            if (canAccessGoal)
                return CreateResponse(goal);

            return Forbid();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterGoalCommand command)
        {
            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateGoalCommand command)
        {
            command.GoalId = id;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            RemoveGoalCommand command = new RemoveGoalCommand
            {
                GoalId = id
            };

            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}/complete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetCompletedAsync(Guid id, [FromBody]SetCompletedGoalCommand command)
        {
            command.GoalId = id;

            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}/uncomplete")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetUncompletedAsync(Guid id)
        {
            SetUncompletedGoalCommand command = new SetUncompletedGoalCommand
            {
                GoalId = id
            };

            return await CreateCommandResponse(command);
        }
    }
}