using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Commands.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Context;
using NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate;
using NutrientAuto.CrossCutting.UnitOfwork.Abstractions;
using NutrientAuto.CrossCutting.UnitOfWork;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community.Admin
{
    [Authorize(Policy = "AdminAccount")]
    [Produces("application/json")]
    [Route("api/admin/measure-categories")]
    public class MeasureCategoriesController : BaseController
    {
        private readonly IMeasureCategoryRepository _measureCategoryRepository;
        private readonly IUnitOfWork<ICommunityDbContext> _unitOfWork;

        public MeasureCategoriesController(IMeasureCategoryRepository measureCategoryRepository, IUnitOfWork<ICommunityDbContext> unitOfWork, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<MeasureCategoriesController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _measureCategoryRepository = measureCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<MeasureCategory>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            List<MeasureCategory> measureCategories = await _measureCategoryRepository.GetAllDefaultsAsync();

            return CreateResponse(measureCategories);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(MeasureCategory), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            MeasureCategory measureCategory = await _measureCategoryRepository.GetDefaultByIdAsync(id);

            return CreateResponse(measureCategory);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterMeasureCategoryCommand command)
        {
            bool validCommand = command.Validate();
            if (!validCommand)
                return CreateErrorResponse(command.ValidationResult);

            MeasureCategory measureCategory = new MeasureCategory(
                command.Name,
                command.Description,
                command.IsFavorite
                );

            await _measureCategoryRepository.RegisterAsync(measureCategory);

            return await CommitAsync();
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateMeasureCategoryCommand command)
        {
            bool validCommand = command.Validate();
            if (!validCommand)
                return CreateErrorResponse(command.ValidationResult);

            MeasureCategory measureCategory = await _measureCategoryRepository.GetDefaultByIdAsync(id);
            if (measureCategory == null)
                return NotFound();

            measureCategory.Update(
                command.Name,
                command.Description,
                command.IsFavorite
                );

            await _measureCategoryRepository.UpdateAsync(measureCategory);

            return await CommitAsync();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            MeasureCategory measureCategory = await _measureCategoryRepository.GetDefaultByIdAsync(id);
            if (measureCategory == null)
                return NotFound();

            await _measureCategoryRepository.RemoveAsync(measureCategory);

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