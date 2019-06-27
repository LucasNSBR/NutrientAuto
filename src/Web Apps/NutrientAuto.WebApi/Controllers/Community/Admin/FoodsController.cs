using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.FoodAggregate;
using NutrientAuto.Community.Domain.Aggregates.SeedWork;
using NutrientAuto.Community.Domain.Commands.FoodAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.FoodAggregate;
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
    [Route("api/admin/foods")]
    public class FoodsController : BaseController
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ICommunityDbContext> _unitOfWork;

        public FoodsController(IFoodRepository foodRepository, IMapper mapper, IUnitOfWork<ICommunityDbContext> unitOfWork, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<FoodsController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Food> foods = await _foodRepository.GetAllDefaultsAsync();

            return CreateResponse(foods);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            Food food = await _foodRepository.GetDefaultByIdAsync(id);

            return CreateResponse(food);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterFoodCommand command)
        {
            bool validCommand = command.Validate();
            if (!validCommand)
                return CreateErrorResponse(command.ValidationResult);

            Food food = new Food(
                command.Name,
                command.Description,
                command.FoodTableId,
                _mapper.Map<MacronutrientTable>(command.Macronutrients),
                _mapper.Map<MicronutrientTable>(command.Micronutrients),
                new FoodUnit(command.UnitType, command.DefaultGramsQuantityMultiplier)
                );

            await _foodRepository.RegisterAsync(food);

            return await CommitAsync();
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateFoodCommand command)
        {
            bool validCommand = command.Validate();
            if (!validCommand)
                return CreateErrorResponse(command.ValidationResult);

            Food food = await _foodRepository.GetDefaultByIdAsync(id);
            if (food == null)
                return NotFound();

            food.Update(
                command.Name,
                command.Description,
                command.FoodTableId,
                _mapper.Map<MacronutrientTable>(command.Macronutrients),
                _mapper.Map<MicronutrientTable>(command.Micronutrients),
                new FoodUnit(command.UnitType, command.DefaultGramsQuantityMultiplier)
                );

            await _foodRepository.UpdateAsync(food);

            return await CommitAsync();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            Food food = await _foodRepository.GetDefaultByIdAsync(id);
            if (food == null)
                return NotFound();

            await _foodRepository.RemoveAsync(food);

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