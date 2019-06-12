using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Commands.ProfileAggregate;
using NutrientAuto.Community.Domain.ReadModels.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
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
    [Route("api/profiles")]
    public class ProfilesController : BaseController
    {
        private readonly IProfileReadModelRepository _readModelRepository;
        private readonly Guid _currentProfileId;

        public ProfilesController(IProfileReadModelRepository readModelRepository, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<ProfilesController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _readModelRepository = readModelRepository;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<ProfileListReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProfileListAsync(string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            IEnumerable<ProfileListReadModel> profiles = await _readModelRepository.GetProfileListAsync(nameFilter, pageNumber, pageSize);

            return CreateResponse(profiles);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(ProfileSummaryReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProfileSummaryAsync(Guid id)
        {
            ProfileSummaryReadModel profile = await _readModelRepository.GetProfileSummaryAsync(id);

            return CreateResponse(profile);
        }

        [HttpGet]
        [Route("me")]
        [ProducesResponseType(typeof(ProfileOverviewReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProfileOverviewAsync()
        {
            ProfileOverviewReadModel profile = await _readModelRepository.GetProfileOverviewAsync(_currentProfileId);

            return CreateResponse(profile);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateProfileCommand command)
        {
            command.ProfileId = id;

            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}/avatar")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SetAvatarImageAsync(Guid id, [FromBody]SetAvatarImageCommand command)
        {
            command.ProfileId = id;

            return await CreateCommandResponse(command);
        }

        [HttpGet]
        [Route("me/settings")]
        [ProducesResponseType(typeof(ProfileSettingsReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProfileSettingsAsync()
        {
            ProfileSettingsReadModel profile = await _readModelRepository.GetProfileSettingsAsync(_currentProfileId);

            return CreateResponse(profile);
        }

        [HttpPut]
        [Route("{id:guid}/settings")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeSettingsAsync(Guid id, [FromBody]ChangeSettingsCommand command)
        {
            command.ProfileId = id;

            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}/add-friend")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendFriendshipRequestAsync(Guid id, [FromBody]RegisterFriendshipRequestCommand command)
        {
            command.RequestedId = id;

            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}/remove-friend")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UnfriendAsync(Guid id)
        {
            UnfriendProfileCommand command = new UnfriendProfileCommand
            {
                FriendProfileId = id
            };

            return await CreateCommandResponse(command);
        }
    }
}