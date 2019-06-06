using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.ReadModels.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate;
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
    [Route("api/friendships")]
    public class FriendshipRequestsController : BaseController
    {
        private readonly IFriendshipRequestReadModelRepository _friendshipRequestReadModelRepository;
        private readonly IIdentityService _identityService;

        public FriendshipRequestsController(IFriendshipRequestReadModelRepository friendshipRequestReadModelRepository, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<FriendshipRequestsController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _friendshipRequestReadModelRepository = friendshipRequestReadModelRepository;
            _identityService = identityService;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<FriendshipRequestListReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFriendshipRequestList(string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            Guid requestedId = _identityService.GetUserId();

            IEnumerable<FriendshipRequestListReadModel> friendshipRequests = await _friendshipRequestReadModelRepository
                .GetFriendshipRequestList(requestedId, nameFilter, pageNumber, pageSize);

            return CreateResponse(friendshipRequests);
        }

        [HttpGet]
        [Route("sent")]
        [ProducesResponseType(typeof(List<FriendshipRequestSentListReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFriendshipRequestSentList(string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            Guid requesterId = _identityService.GetUserId();

            IEnumerable<FriendshipRequestSentListReadModel> friendshipRequestsSent = await _friendshipRequestReadModelRepository
                .GetFriendshipRequestSentList(requesterId, nameFilter, pageNumber, pageSize);

            return CreateResponse(friendshipRequestsSent);
        }

        [HttpPut]
        [Route("{id:guid}/accept")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AcceptFriendshipRequestAsync(Guid id)
        {
            AcceptFriendshipRequestCommand command = new AcceptFriendshipRequestCommand
            {
                FriendshipRequestId = id
            };

            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}/reject")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RejectFriendshipRequestAsync(Guid id)
        {
            RejectFriendshipRequestCommand command = new RejectFriendshipRequestCommand
            {
                FriendshipRequestId = id
            };

            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}/cancel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CancelFriendshipRequestAsync(Guid id)
        {
            CancelFriendshipRequestCommand command = new CancelFriendshipRequestCommand
            {
                FriendshipRequestId = id
            };

            return await CreateCommandResponse(command);
        }
    }
}