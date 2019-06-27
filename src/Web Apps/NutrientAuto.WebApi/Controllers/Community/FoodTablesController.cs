using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using NutrientAuto.Community.Domain.Commands.FoodTableAggregate;
using NutrientAuto.Community.Domain.Repositories.FoodTableAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Authorize("ActiveProfile")]
    [Produces("application/json")]
    [Route("api/food-tables")]
    public class FoodTablesController : BaseController
    {
        private readonly IFoodTableRepository _foodTableRepository;
        private readonly Guid _currentProfileId;

        public FoodTablesController(IFoodTableRepository foodTableRepository, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<FoodTablesController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _foodTableRepository = foodTableRepository;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<FoodTable> foodTables = await _foodTableRepository.GetAllByProfileIdAsync(_currentProfileId);

            return CreateResponse(foodTables);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            FoodTable foodTable = await _foodTableRepository.GetByIdAndProfileIdAsync(id, _currentProfileId);

            return CreateResponse(foodTable);
        }

        [HttpGet]
        [Route("my-food-tables")]
        public async Task<IActionResult> GetAllByProfileIdAsync()
        {
            List<CustomFoodTable> customFoodTables = await _foodTableRepository.GetAllCustomsByProfileIdAsync(_currentProfileId);

            return CreateResponse(customFoodTables);
        }

        [HttpGet]
        [Route("my-food-tables/{id:guid}")]
        public async Task<IActionResult> GetCustomByIdAsync(Guid id)
        {
            CustomFoodTable customFoodTable = await _foodTableRepository.GetCustomByIdAsync(id, _currentProfileId);

            return CreateResponse(customFoodTable);
        }

        [HttpPost]
        [Route("my-food-tables")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterFoodTableCommand command)
        {
            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("my-food-tables/{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateFoodTableCommand command)
        {
            command.FoodTableId = id;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("my-food-tables/{id:guid}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            RemoveFoodTableCommand command = new RemoveFoodTableCommand
            {
                FoodTableId = id,
            };

            return await CreateCommandResponse(command);
        }
    }
}