using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Commands.MeasureAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.ReadModels.MeasureAggregate;
using NutrientAuto.Community.Domain.Repositories.MeasureAggregate;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Produces("application/json")]
    //[Authorize(Policy = "ActiveProfile")]
    [Route("api/measures")]
    public class MeasuresController : BaseController
    {
        private readonly IMeasureReadModelRepository _measureReadModelRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly Guid _currentProfileId;

        public MeasuresController(IMeasureRepository measureRepository, IMeasureReadModelRepository measureReadModelRepository, IProfileDomainService profileDomainService, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<MeasuresController> logger)
            : base(domainNotificationHandler, mediator, logger)
        {
            _measureReadModelRepository = measureReadModelRepository;
            _profileDomainService = profileDomainService;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("profile/{profileId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<MeasureListReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllByProfileIdAsync(Guid profileId, string titleFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            ProfileAccessResult canAccessMeasures = await _profileDomainService.CanAccessProfileData(_currentProfileId, profileId);

            if (canAccessMeasures == ProfileAccessResult.CanAccess)
            {
                IEnumerable<MeasureListReadModel> measures = await _measureReadModelRepository.GetMeasureListAsync(profileId, titleFilter, pageNumber, pageSize);
                return CreateResponse(measures);
            }
            else if (canAccessMeasures == ProfileAccessResult.Forbidden) return Forbid();

            return NotFound();
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(MeasureSummaryReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            MeasureSummaryReadModel measure = await _measureReadModelRepository.GetMeasureSummaryAsync(id);

            if (measure != null)
            {
                ProfileAccessResult canAccessMeasure = await _profileDomainService.CanAccessProfileData(_currentProfileId, measure.ProfileId);
                if (canAccessMeasure == ProfileAccessResult.CanAccess)
                    return CreateResponse(measure);
                if (canAccessMeasure == ProfileAccessResult.Forbidden)
                    return Forbid();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterMeasureCommand command)
        {
            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateMeasureCommand command)
        {
            command.MeasureId = id;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            RemoveMeasureCommand command = new RemoveMeasureCommand
            {
                MeasureId = id
            };

            return await CreateCommandResponse(command);
        }
    }
}