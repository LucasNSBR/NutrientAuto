using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Commands.FoodAggregate;
using NutrientAuto.Community.Domain.Repositories.FoodAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Produces("application/json")]
    //[Authorize(Policy = "ActiveProfile")]
    [Route("api/foods")]
    public class FoodsController : BaseController
    {
        private readonly IFoodRepository _foodRepository;
        private readonly Guid _currentProfileId;

        public FoodsController(IFoodRepository foodRepository, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<FoodsController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _foodRepository = foodRepository;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Food> foods = await _foodRepository.GetAllByProfileIdAsync(_currentProfileId);

            return CreateResponse(foods);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Food food = await _foodRepository.GetByIdAndProfileIdAsync(id, _currentProfileId);

            return CreateResponse(food);
        }

        [HttpGet]
        [Route("my-foods")]
        public async Task<IActionResult> GetAllByProfileIdAsync()
        {
            List<CustomFood> customFoods = await _foodRepository.GetAllCustomsByProfileIdAsync(_currentProfileId);

            return CreateResponse(customFoods);
        }

        [HttpGet]
        [Route("my-foods/{id:guid}")]
        public async Task<IActionResult> GetCustomByIdAsync(Guid id)
        {
            CustomFood customFood = await _foodRepository.GetCustomByIdAsync(id, _currentProfileId);

            return CreateResponse(customFood);
        }

        [HttpPost]
        [Route("my-foods")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterFoodCommand command)
        {
            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("my-foods/{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateFoodCommand command)
        {
            command.FoodId = id;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("my-foods/{id:guid}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            RemoveFoodCommand command = new RemoveFoodCommand
            {
                FoodId = id,
            };

            return await CreateCommandResponse(command);
        }
    }
}