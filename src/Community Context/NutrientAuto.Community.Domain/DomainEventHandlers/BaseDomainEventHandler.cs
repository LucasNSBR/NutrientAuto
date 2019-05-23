using Microsoft.Extensions.Logging;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Community.Domain.Context;
using System.Threading.Tasks;

namespace NutrientAuto.Community.Domain.DomainEventHandlers
{
    public abstract class BaseDomainEventHandler
    {
        protected readonly IUnitOfWork<ICommunityDbContext> _unitOfWork;
        protected readonly ILogger _logger;

        protected BaseDomainEventHandler(IUnitOfWork<ICommunityDbContext> unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        protected async Task<bool> CommitAsync()
        {
            CommitResult commitResult = await _unitOfWork.CommitAsync();

            if (commitResult.Success)
                return true;

            return false;
        }
    }
}
