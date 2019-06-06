using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.CommentAggregate;
using NutrientAuto.Community.Domain.Aggregates.PostAggregate;
using NutrientAuto.Community.Domain.Commands.CommentAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.CommentAggregate;
using NutrientAuto.Community.Domain.Repositories.PostAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.CommentAggregate
{
    public class CommentCommandHandler : ContextCommandHandler,
                                         IRequestHandler<ReplyCommentCommand, CommandResult>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly Guid _currentProfileId;

        public CommentCommandHandler(ICommentRepository commentRepository, IPostRepository postRepository, IProfileDomainService profileDomainService, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<CommentCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _profileDomainService = profileDomainService;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(ReplyCommentCommand request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetByIdAsync(request.PostId);

            ProfileAccessResult accessResult = await _profileDomainService.CanAccessProfileData(_currentProfileId, post.ProfileId);
            if (accessResult != ProfileAccessResult.CanAccess)
                return FailureDueToPostNotFound();

            Comment comment = await _commentRepository.GetByIdAsync(request.CommentId);
            if (comment == null)
                return FailureDueToCommentNotFound();

            comment.Reply(
                _currentProfileId,
                request.Body
                );

            await _commentRepository.UpdateAsync(comment);

            return await CommitAndPublishDefaultAsync();
        }

        private CommandResult FailureDueToCommentNotFound()
        {
            return FailureDueToEntityNotFound("Id do comentário inválido", "Falha ao buscar comentário no banco de dados.");
        }
    }
}
