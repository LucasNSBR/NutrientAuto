using MassTransit;
using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Community.Domain.Aggregates.ProfileAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.ProfileAggregate;
using NutrientAuto.Shared.IntegrationEvents.Events.Identity;
using NutrientAuto.Shared.ValueObjects;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.IntegrationEventHandlers.Identity
{
    public class IdentityIntegrationEventHandler : IConsumer<UserRegisteredIntegrationEvent>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork<ICommunityDbContext> _unitOfWork;
        private readonly ILogger<IdentityIntegrationEventHandler> _logger;

        public IdentityIntegrationEventHandler(IProfileRepository profileRepository, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<IdentityIntegrationEventHandler> logger)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            Profile profile = new Profile(
                context.Message.UserId,
                context.Message.Genre,
                context.Message.Name,
                context.Message.Username,
                new EmailAddress(context.Message.Email),
                context.Message.BirthDate);

            await _profileRepository.RegisterAsync(profile);

            CommitResult result = await _unitOfWork.CommitAsync();
            if (!result.Success)
            {
                _logger.LogCritical(result.Exception, "Falha ao criar Profile em [Community]: {exception}", result.Exception.Message);
                throw result.Exception;
            }
        }
    }
}
