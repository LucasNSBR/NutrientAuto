using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Commands.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.Repositories.FriendshipRequestAggregate;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.Shared.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.CommandHandlers.FriendshipRequestAggregate
{
    public partial class FriendshipRequestCommandHandler : ContextCommandHandler,
                                                           IRequestHandler<RegisterFriendshipRequestCommand, CommandResult>,
                                                           IRequestHandler<AcceptFriendshipRequestCommand, CommandResult>,
                                                           IRequestHandler<RejectFriendshipRequestCommand, CommandResult>,
                                                           IRequestHandler<CancelFriendshipRequestCommand, CommandResult>
    {
        private readonly IFriendshipRequestRepository _friendshipRequestRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly Guid _currentProfileId;

        public FriendshipRequestCommandHandler(IFriendshipRequestRepository friendshipRequestRepository, IProfileRepository profileRepository, IProfileDomainService profileDomainService, IIdentityService identityService, IMediator mediator, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<FriendshipRequestCommandHandler> logger)
            : base(identityService, mediator, unitOfWork, logger)
        {
            _friendshipRequestRepository = friendshipRequestRepository;
            _profileRepository = profileRepository;
            _profileDomainService = profileDomainService;
            _currentProfileId = GetCurrentProfileId();
        }

        public async Task<CommandResult> Handle(RegisterFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            FriendshipRequest existingFriendshipRequest = await _friendshipRequestRepository.GetByCompositeIdAsync(_currentProfileId, request.RequestedId);
            if (existingFriendshipRequest != null)
                return FailureDueTo("Falha ao criar nova solicitação", "Você já enviou uma solicitação de amizade para esse usuário.");

            FriendshipRequest friendshipRequest = new FriendshipRequest(
                _currentProfileId,
                request.RequestedId,
                request.RequestBody
                );

            await _friendshipRequestRepository.RegisterAsync(friendshipRequest);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(AcceptFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            FriendshipRequest friendshipRequest = await _friendshipRequestRepository.GetByIdAsync(request.FriendshipRequestId);
            if (friendshipRequest == null || !friendshipRequest.IsRequested(_currentProfileId))
                return FailureDueToFriendshipNotFound();

            CommandResult profileServiceResult = await _profileDomainService.MakeFriends(friendshipRequest.RequesterId, friendshipRequest.RequestedId);
            if (!profileServiceResult.Success)
                return profileServiceResult;

            friendshipRequest.Accept();
            if (!friendshipRequest.IsValid)
                return FailureDueToEntityStateInconsistency(friendshipRequest);

            await _friendshipRequestRepository.UpdateAsync(friendshipRequest);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(RejectFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            FriendshipRequest friendshipRequest = await _friendshipRequestRepository.GetByIdAsync(request.FriendshipRequestId);
            if (friendshipRequest == null || !friendshipRequest.IsRequested(_currentProfileId))
                return FailureDueToFriendshipNotFound();

            await _friendshipRequestRepository.RemoveAsync(friendshipRequest);

            return await CommitAndPublishDefaultAsync();
        }

        public async Task<CommandResult> Handle(CancelFriendshipRequestCommand request, CancellationToken cancellationToken)
        {
            FriendshipRequest friendshipRequest = await _friendshipRequestRepository.GetByIdAsync(request.FriendshipRequestId);
            if (friendshipRequest == null || !friendshipRequest.IsRequester(_currentProfileId))
                return FailureDueToFriendshipNotFound();

            if (!friendshipRequest.IsPending)
                return FailureDueTo("Solicitação Inválida", "Não é possível remover uma solicitação de amizade que já foi aceita.");

            await _friendshipRequestRepository.RemoveAsync(friendshipRequest);

            return await CommitAndPublishDefaultAsync();
        }
        
        private CommandResult FailureDueToFriendshipNotFound()
        {
            return FailureDueToEntityNotFound("Id da Solicitação inválido", "Falha ao buscar solicitação de amizade no banco de dados.");
        }
    }
}