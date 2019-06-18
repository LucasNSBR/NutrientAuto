using MediatR;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.DietAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.DomainEvents.MealAggregate;
using NutrientAuto.Community.Domain.Repositories.DietAggregate;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainEventHandlers.DietAggregate
{
    public class DietDomainEventHandler : BaseDomainEventHandler,
                                          INotificationHandler<MealFoodAddedDomainEvent>,
                                          INotificationHandler<MealFoodRemovedDomainEvent>
    {
        private readonly IDietRepository _dietRepository;

        public DietDomainEventHandler(IDietRepository dietRepository, IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger<DietDomainEventHandler> logger)
            : base(unitOfWork, logger)
        {
            _dietRepository = dietRepository;
        }

        public async Task Handle(MealFoodAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            await RecalculateDietTotalMacrosAsync(notification.DietId);
        }

        public async Task Handle(MealFoodRemovedDomainEvent notification, CancellationToken cancellationToken)
        {
            await RecalculateDietTotalMacrosAsync(notification.DietId);
        }

        private async Task RecalculateDietTotalMacrosAsync(Guid dietId)
        {
            Diet diet = await _dietRepository.GetByIdAsync(dietId);
            diet.RecalculateDietTotalMacros();

            await _dietRepository.UpdateAsync(diet);
            await CommitAsync();
        }
    }
}
