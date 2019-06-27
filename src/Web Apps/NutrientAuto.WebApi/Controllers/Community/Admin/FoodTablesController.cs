using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.FoodTableAggregate;
using NutrientAuto.Community.Domain.Commands.FoodTableAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.FoodTableAggregate;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community.Admin
{
    [Authorize(Policy = "AdminAccount")]
    [Produces("application/json")]
    [Route("api/admin/food-tables")]
    public class FoodTablesController : BaseController
    {
        private readonly IFoodTableRepository _foodTableRepository;
        private readonly IUnitOfWork<ICommunityDbContext> _unitOfWork;

        public FoodTablesController(IFoodTableRepository foodTableRepository, IUnitOfWork<ICommunityDbContext> unitOfWork, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<FoodTablesController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _foodTableRepository = foodTableRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<FoodTable> foodTables = await _foodTableRepository.GetAllDefaultsAsync();

            return CreateResponse(foodTables);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            FoodTable foodTable = await _foodTableRepository.GetDefaultByIdAsync(id);

            return CreateResponse(foodTable);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterFoodTableCommand command)
        {
            bool validCommand = command.Validate();
            if (!validCommand)
                return CreateErrorResponse(command.ValidationResult);

            FoodTable foodTable = new FoodTable(
                command.Name,
                command.Description
                );

            await _foodTableRepository.RegisterAsync(foodTable);

            return await CommitAsync();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateFoodTableCommand command)
        {
            bool validCommand = command.Validate();
            if (!validCommand)
                return CreateErrorResponse(command.ValidationResult);

            FoodTable foodTable = await _foodTableRepository.GetDefaultByIdAsync(id);
            if (foodTable == null)
                return NotFound();

            foodTable.Update(
                command.Name,
                command.Description
                );

            await _foodTableRepository.UpdateAsync(foodTable);

            return await CommitAsync();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            FoodTable foodTable = await _foodTableRepository.GetDefaultByIdAsync(id);
            if (foodTable == null)
                return NotFound();

            await _foodTableRepository.RemoveAsync(foodTable);

            return await CommitAsync();
        }

        private async Task<IActionResult> CommitAsync()
        {
            CommitResult result = await _unitOfWork.CommitAsync();
            if (!result.Success)
                return BadRequest(result.Exception.Message);

            return Ok();
        }
    }
}