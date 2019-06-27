using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Authorize("ActiveProfile")]
    [Produces("application/json")]
    [Route("api/measure-categories")]
    public class MeasureCategoriesController : BaseController
    {
        private readonly IMeasureCategoryRepository _measureCategoryRepository;
        private readonly Guid _currentProfileId;

        public MeasureCategoriesController(IMeasureCategoryRepository measureCategoryRepository, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<FoodTablesController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _measureCategoryRepository = measureCategoryRepository;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<MeasureCategory> measureCategories = await _measureCategoryRepository.GetAllByProfileIdAsync(_currentProfileId);

            return CreateResponse(measureCategories);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            MeasureCategory measureCategory = await _measureCategoryRepository.GetByIdAndProfileIdAsync(id, _currentProfileId);

            return CreateResponse(measureCategory);
        }

        [HttpGet]
        [Route("my-measure-categories")]
        public async Task<IActionResult> GetAllByProfileIdAsync()
        {
            List<CustomMeasureCategory> customMeasureCategories = await _measureCategoryRepository.GetAllCustomsByProfileIdAsync(_currentProfileId);

            return CreateResponse(customMeasureCategories);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetAllFavoritesByProfileIdAsync()
        {
            List<MeasureCategory> measureCategories = await _measureCategoryRepository.GetAllFavoritesByProfileIdAsync(_currentProfileId);

            return CreateResponse(measureCategories);
        }

        [HttpGet]
        [Route("my-measure-categories/{id:guid}")]
        public async Task<IActionResult> GetCustomByIdAsync(Guid id)
        {
            CustomMeasureCategory customMeasureCategory = await _measureCategoryRepository.GetCustomByIdAsync(id, _currentProfileId);

            return CreateResponse(customMeasureCategory);
        }

        [HttpPost]
        [Route("my-measure-categories")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterMeasureCategoryCommand command)
        {
            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("my-measure-categories/{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateMeasureCategoryCommand command)
        {
            command.MeasureCategoryId = id;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("my-measure-categories/{id:guid}")]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            RemoveMeasureCategoryCommand command = new RemoveMeasureCategoryCommand
            {
                MeasureCategoryId = id,
            };

            return await CreateCommandResponse(command);
        }
    }
}