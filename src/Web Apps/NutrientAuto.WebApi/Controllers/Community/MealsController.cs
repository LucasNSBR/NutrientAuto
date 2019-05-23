using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Commands.MealAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.ReadModels.MealAggregate;
using NutrientAuto.Community.Domain.Repositories.MealAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Produces("application/json")]
    //[Authorize(Policy = "ActiveProfile")]
    [Route("api/diets/meals")]
    public class MealsController : BaseController
    {
        private readonly IMealReadModelRepository _readModelRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly Guid _currentProfileId;

        public MealsController(IMealReadModelRepository readModelRepository, IProfileDomainService profileDomainService, IIdentityService identityService, IMediator mediator, IDomainNotificationHandler domainNotificationHandler, ILogger<MealsController> logger)
           : base(domainNotificationHandler, mediator, logger)
        {
            _readModelRepository = readModelRepository;
            _profileDomainService = profileDomainService;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(MealSummaryReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMealSummaryAsync(Guid id)
        {
            MealSummaryReadModel meal = await _readModelRepository.GetMealSummaryAsync(id);

            bool canAccessMeal = await _profileDomainService.CanAccessProfileData(_currentProfileId, meal.ProfileId);
            if (canAccessMeal)
                return CreateResponse(meal);

            return Forbid();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateMealCommand command)
        {
            command.MealId = id;

            return await CreateCommandResponse(command);
        }

        [HttpPost]
        [Route("{id:guid}/add-food")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddMealFoodAsync(Guid id, [FromBody]AddMealFoodCommand command)
        {
            command.MealId = id;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("{mealId:guid}/remove-food/{mealFoodId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveMealAsync(Guid mealId, Guid mealFoodId)
        {
            RemoveMealFoodCommand command = new RemoveMealFoodCommand
            {
                MealId = mealId,
                MealFoodId = mealFoodId,
            };

            return await CreateCommandResponse(command);
        }
    }
}