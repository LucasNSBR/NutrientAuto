using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NutrientAuto.Community.Domain.CommandValidators.SeedWork;
using NutrientAuto.CrossCutting.HttpService.HttpContext;
using NutrientAuto.CrossCutting.Storage.Services.StorageService;
using NutrientAuto.Shared.Notifications;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NutrientAuto.WebApi.Controllers.Community
{
    [Produces("application/json")]
    //[Authorize(Policy = "ActiveProfile")]
    [Route("api/storage")]
    public class StorageController : BaseController
    {
        private readonly IStorageService _storageService;
        private readonly IIdentityService _identityService;

        public StorageController(IIdentityService identityService, IStorageService storageService, IDomainNotificationHandler domainNotificationHandler, IMediator mediator, ILogger logger) : base(domainNotificationHandler, mediator, logger)
        {
            _identityService = identityService;
            _storageService = storageService;
        }

        [HttpPost]
        [Route("measure")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SaveMeasurePhotoAsync(IFormFile formFile)
        {
            return await CreateUploadResponse(formFile, $"measure-photo-{Guid.NewGuid().ToString()}");
        }

        [HttpPost]
        [Route("post")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SavePostPhotoAsync(IFormFile formFile)
        {
            return await CreateUploadResponse(formFile, $"post-photo-{Guid.NewGuid().ToString()}");
        }

        private bool NotifyFormFileErrors(IFormFile formFile)
        {
            ValidationResult validationResult = new FormFileValidator().Validate(formFile);
            if (!validationResult.IsValid)
                AddNotifications(validationResult.Errors.Select(error => new DomainNotification(error.ErrorCode, error.ErrorMessage)).ToList());

            return validationResult.IsValid;
        }

        private async Task<Image> UploadFileToStorageAsync(IFormFile formFile, string imageName)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);

                StorageResult result = await _storageService.UploadFileToStorageAsync(stream, imageName);
                if (!result.Success)
                    return null;

                return new Image(result.FileUrl, result.FileName);
            }
        }

        private async Task<IActionResult> CreateUploadResponse(IFormFile formFile, string imageName)
        {
            if (NotifyFormFileErrors(formFile))
                return CreateErrorResponse();

            Image image = await UploadFileToStorageAsync(formFile, imageName);
            if (image == null)
                return CreateErrorResponse("Falha de upload", "Falha ao fazer upload do arquivo para o servidor.");

            return CreateResponse(image);
        }
    }
}