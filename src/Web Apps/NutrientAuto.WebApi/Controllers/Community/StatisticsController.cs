using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Aggregates.MeasureCategoryAggregate;
using NutrientAuto.Community.Domain.Aggregates.MeasureStatisticsAggregate;
using NutrientAuto.Community.Domain.DomainServices.MeasureStatisticsAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureCategoryAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Authorize("ActiveProfile")]
    [Produces("application/json")]
    [Route("api/statistics")]
    public class StatisticsController : BaseController
    {
        private readonly IMeasureCategoryRepository _measureCategoryRepository;
        private readonly IMeasureStatisticsDomainService _measureStatisticsDomainService;
        private readonly Guid _currentProfileId;

        public StatisticsController(IMeasureCategoryRepository measureCategoryRepository, IMeasureStatisticsDomainService measureStatisticsDomainService, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<StatisticsController> logger)
           : base(domainNotificationHandler, mediator, logger)
        {
            _measureCategoryRepository = measureCategoryRepository;
            _measureStatisticsDomainService = measureStatisticsDomainService;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<CategoryStatistics>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasicEntriesAndByCategoriesAsync(DateTime startDate, DateTime endDate, [FromBody]List<Guid> categoryIds)
        {
            List<MeasureCategory> measureCategories = new List<MeasureCategory>();

            foreach (Guid categoryId in categoryIds)
            {
                MeasureCategory measureCategory = await _measureCategoryRepository.GetByIdAndProfileIdAsync(categoryId, _currentProfileId);
                if (measureCategory == null)
                    return CreateErrorResponse("Erro de categoria", $"Nenhuma categoria de medição com esse Id {categoryId} foi encontrada no banco de dados.");

                measureCategories.Add(measureCategory);
            }

            StatisticsWrapper wrapper = await _measureStatisticsDomainService.GetBasicEntriesAndByCategoriesAsync(measureCategories, startDate, endDate);

            return CreateResponse(wrapper);
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(List<CategoryStatistics>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEntriesByCategoriesAsync(DateTime startDate, DateTime endDate, [FromBody]List<Guid> categoryIds)
        {
            List<MeasureCategory> measureCategories = new List<MeasureCategory>();

            foreach (Guid categoryId in categoryIds)
            {
                MeasureCategory measureCategory = await _measureCategoryRepository.GetByIdAndProfileIdAsync(categoryId, _currentProfileId);
                if (measureCategory == null)
                    return CreateErrorResponse("Erro de categoria", $"Nenhuma categoria de medição com esse Id {categoryId} foi encontrada no banco de dados.");

                measureCategories.Add(measureCategory);
            }

            List<CategoryStatistics> statistics = await _measureStatisticsDomainService.GetEntriesByCategoriesAsync(measureCategories, startDate, endDate);

            return CreateResponse(statistics);
        }

        [HttpGet]
        [Route("{categoryId:guid}")]
        [ProducesResponseType(typeof(CategoryStatistics), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEntriesByCategoryAsync(Guid categoryId, DateTime startDate, DateTime endDate)
        {
            MeasureCategory measureCategory = await _measureCategoryRepository.GetByIdAndProfileIdAsync(categoryId, _currentProfileId);
            if (measureCategory == null)
                return CreateErrorResponse("Erro de categoria", "Nenhuma categoria de medição com esse Id foi encontrada no banco de dados.");

            CategoryStatistics statistics = await _measureStatisticsDomainService.GetEntriesByCategoryAsync(measureCategory, startDate, endDate);

            return CreateResponse(statistics);
        }
    }
}