using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageValidators
{
    public class ImageStorageValidator : StorageValidator
    {
        public override StorageValidatorResult Validate(IFormFile formFile)
        {
            List<StorageValidatorError> errors = new List<StorageValidatorError>();

            if (formFile.FileName == null || (formFile.FileName.Length < 1 || formFile.FileName.Length > 100))
                errors.Add(new StorageValidatorError("Nome do arquivo", "O nome da imagem deve ter de 1 até 100 caracteres."));

            if (formFile.ContentType.ToLower() != "image/jpg" &&
                formFile.ContentType.ToLower() != "image/jpeg" &&
                formFile.ContentType.ToLower() != "image/x-png" &&
                formFile.ContentType.ToLower() != "image/png")
                errors.Add(new StorageValidatorError("Formato do arquivo", "O formato da imagem deve ser .jpg, .jpeg ou .png."));

            int fiveMegaBytes = 1024 * 1024 * 5;

            if (formFile.Length <= 0 || formFile.Length > fiveMegaBytes)
                errors.Add(new StorageValidatorError("Tamanho do arquivo", "O tamanho do arquivo máximo da imagem deve ser 5mb."));

            return new StorageValidatorResult(false, errors);
        }
    }
}
