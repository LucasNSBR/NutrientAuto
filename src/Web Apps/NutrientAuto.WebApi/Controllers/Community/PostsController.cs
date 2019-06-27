using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Commands.CommentAggregate;
using NutrientAuto.Community.Domain.Commands.PostAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.ReadModels.PostAggregate;
using NutrientAuto.Community.Domain.Repositories.PostAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/posts")]
    public class PostsController : BaseController
    {
        private readonly IPostReadModelRepository _postReadModelRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly Guid _currentProfileId;

        public PostsController(IPostRepository postRepository, IPostReadModelRepository postReadModelRepository, IProfileDomainService profileDomainService, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<PostsController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _postReadModelRepository = postReadModelRepository;
            _profileDomainService = profileDomainService;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("profile/{profileId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<PostListReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllByProfileIdAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            ProfileAccessResult canAccessPosts = await _profileDomainService.CanAccessProfileData(_currentProfileId, profileId);

            if (canAccessPosts == ProfileAccessResult.CanAccess)
            {
                IEnumerable<PostListReadModel> posts = await _postReadModelRepository.GetPostListAsync(profileId, titleFilter, pageNumber, pageSize);
                return CreateResponse(posts);
            }
            else if (canAccessPosts == ProfileAccessResult.Forbidden) return Forbid();


            return NotFound();
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(PostSummaryReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            PostSummaryReadModel post = await _postReadModelRepository.GetPostSummaryAsync(id);

            if (post != null)
            {
                ProfileAccessResult canAccessPost = await _profileDomainService.CanAccessProfileData(_currentProfileId, post.ProfileId);
                if (canAccessPost == ProfileAccessResult.CanAccess)
                    return CreateResponse(post);
                if (canAccessPost == ProfileAccessResult.Forbidden)
                    return Forbid();
            }

            return NotFound();
        }

        [Authorize("ActiveProfile")]
        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterPostCommand command)
        {
            return await CreateCommandResponse(command);
        }

        [Authorize("ActiveProfile")]
        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            RemovePostCommand command = new RemovePostCommand
            {
                PostId = id
            };

            return await CreateCommandResponse(command);
        }

        [Authorize("ActiveProfile")]
        [HttpPut]
        [Route("{id:guid}/like")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddLikeAsync(Guid id)
        {
            AddLikeCommand command = new AddLikeCommand
            {
                PostId = id
            };

            return await CreateCommandResponse(command);
        }

        [Authorize("ActiveProfile")]
        [HttpPut]
        [Route("{id:guid}/unlike")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveLikeAsync(Guid id)
        {
            RemoveLikeCommand command = new RemoveLikeCommand
            {
                PostId = id
            };

            return await CreateCommandResponse(command);
        }

        [Authorize("ActiveProfile")]
        [HttpPut]
        [Route("{id:guid}/add-comment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddCommentAsync(Guid id, [FromBody]AddCommentCommand command)
        {
            command.PostId = id;

            return await CreateCommandResponse(command);
        }

        [Authorize("ActiveProfile")]
        [HttpPut]
        [Route("{id:guid}/remove-comment/{commentId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveCommentAsync(Guid id, Guid commentId)
        {
            RemoveCommentCommand command = new RemoveCommentCommand
            {
                PostId = id,
                CommentId = commentId
            };

            return await CreateCommandResponse(command);
        }

        [Authorize("ActiveProfile")]
        [HttpPut]
        [Route("{id:guid}/reply-comment/{commentId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReplyCommentAsync(Guid id, Guid commentId, [FromBody]ReplyCommentCommand command)
        {
            command.PostId = id;
            command.CommentId = commentId;

            return await CreateCommandResponse(command);
        }
    }
}