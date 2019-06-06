using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.Commands.DietAggregate;
using NutrientAuto.Community.Domain.DomainServices.ProfileAggregate;
using NutrientAuto.Community.Domain.ReadModels.DietAggregate;
using NutrientAuto.Community.Domain.Repositories.DietAggregate;
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
    [Route("api/diets")]
    public class DietsController : BaseController
    {
        private readonly IDietReadModelRepository _readModelRepository;
        private readonly IProfileDomainService _profileDomainService;
        private readonly Guid _currentProfileId;

        public DietsController(IDietReadModelRepository readModelRepository, IProfileDomainService profileDomainService, IIdentityService identityService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger<DietsController> logger)
           : base(domainNotificationHandler, mediator, logger)
        {
            _readModelRepository = readModelRepository;
            _profileDomainService = profileDomainService;
            _currentProfileId = identityService.GetUserId();
        }

        [HttpGet]
        [Route("profile/{profileId:guid}")]
        [ProducesResponseType(typeof(List<DietListReadModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllByProfileIdAsync(Guid profileId, string nameFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            ProfileAccessResult canAccessDiets = await _profileDomainService.CanAccessProfileData(_currentProfileId, profileId);

            if (canAccessDiets == ProfileAccessResult.CanAccess)
            {
                IEnumerable<DietListReadModel> diets = await _readModelRepository.GetDietListAsync(profileId, nameFilter, pageNumber, pageSize);
                return CreateResponse(diets);
            }
            else if (canAccessDiets == ProfileAccessResult.Forbidden) return Forbid();

            return NotFound();
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(DietSummaryReadModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDietSummaryAsync(Guid id)
        {
            DietSummaryReadModel diet = await _readModelRepository.GetDietSummaryAsync(id);

            if (diet != null)
            {
                ProfileAccessResult canAccessDiet = await _profileDomainService.CanAccessProfileData(_currentProfileId, diet.ProfileId);
                if (canAccessDiet == ProfileAccessResult.CanAccess)
                    return CreateResponse(diet);
                if (canAccessDiet == ProfileAccessResult.Forbidden)
                    return Forbid();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterDietCommand command)
        {
            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateDietCommand command)
        {
            command.DietId = id;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            RemoveDietCommand command = new RemoveDietCommand
            {
                DietId = id
            };

            return await CreateCommandResponse(command);
        }

        [HttpPut]
        [Route("{dietId:guid}/add-meal")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddMealAsync(Guid dietId, [FromBody]AddDietMealCommand command)
        {
            command.DietId = dietId;

            return await CreateCommandResponse(command);
        }

        [HttpDelete]
        [Route("{dietId:guid}/remove-meal/{mealId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveMealAsync(Guid dietId, Guid mealId)
        {
            RemoveDietMealCommand command = new RemoveDietMealCommand
            {
                DietId = dietId,
                DietMealId = mealId
            };

            return await CreateCommandResponse(command);
        }
    }
}