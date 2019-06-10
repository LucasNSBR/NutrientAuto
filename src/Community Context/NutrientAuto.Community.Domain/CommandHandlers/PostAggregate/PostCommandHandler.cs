using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.CommentAggregate;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.Commands.PostAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.CommentAggregate;
using NutrientAuto.Community.Domain.Repositories.PostAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.PostAggregate
{
    public class PostCommandHandler : ContextCommandHandler,
                                      IRequestHandler<RegisterPostCommand, CommandResult>,
                                      IRequestHandler<RemovePostCommand, CommandResult>,
                                      IRequestHandler<AddLikeCommand, CommandResult>,
                                      IRequestHandler<RemoveLikeCommand, CommandResult>,
                                      IRequestHandler<AddCommentCommand, CommandResult>,
                                      IRequestHandler<RemoveCommentCommand, CommandResult>
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly Guid _currentProfileId;

        public PostCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository, IProfileDomainService profileDomainService, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<PostCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _profileDomainService = profileDomainService;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterPostCommand request, CancellationToken cancellationToken)
        {
            Image image = new Image(request.AttachedImage.UrlPath, request.AttachedImage.ImageName);

            Post post = new Post(
                _currentProfileId,
                request.Title,
                request.Body,
                attachedImage: image
                );

            await _postRepository.RegisterAsync(post);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemovePostCommand request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetByIdAsync(request.PostId);
            if (post == null || post.ProfileId != _currentProfileId)
                return FailureDueToPostNotFound();

            await _postRepository.RemoveAsync(post);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetByIdAsync(request.PostId);

            bool canModify = await CanModifyPost(post);
            if (!canModify)
                return FailureDueToPostNotFound();

            PostLike postLike = new PostLike(_currentProfileId);

            post.AddLike(postLike);
            if (!post.IsValid)
                return FailureDueToEntityStateInconsistency(post);

            await _postRepository.UpdateAsync(post);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetByIdAsync(request.PostId);

            bool canModify = await CanModifyPost(post);
            if (!canModify)
                return FailureDueToPostNotFound();

            PostLike like = post.FindLikeByProfileId(_currentProfileId);

            post.RemoveLike(like);
            if (!post.IsValid)
                return FailureDueToEntityStateInconsistency(post);

            await _postRepository.UpdateAsync(post);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetByIdAsync(request.PostId);

            bool canModify = await CanModifyPost(post);
            if (!canModify)
                return FailureDueToPostNotFound();

            Comment comment = new Comment(
                post.Id,
                _currentProfileId,
                request.Body
                );

            post.AddComment(comment);

            await _postRepository.UpdateAsync(post);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RemoveCommentCommand request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetByIdAsync(request.PostId);

            bool isValidPost = await CanModifyPost(post);
            if (!isValidPost)
                return FailureDueToPostNotFound();

            Comment comment = post.FindCommentById(request.CommentId);

            post.RemoveComment(comment);
            if (!post.IsValid)
                return FailureDueToEntityStateInconsistency(post);

            await _commentRepository.RemoveAsync(comment);
            await _postRepository.UpdateAsync(post);

            return await CommitAndPublishDefaultAsync();
        }

        private async Task<bool> CanModifyPost(Post post)
        {
            if (post == null)
                return false;

            ProfileAccessResult accessResult = await _profileDomainService.CanAccessProfileData(_currentProfileId, post.ProfileId);
            return accessResult == ProfileAccessResult.CanAccess;
        }
    }
}
