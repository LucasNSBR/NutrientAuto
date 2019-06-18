using Microsoft.AspNetCore.Http;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageValidators
{
    public abstract class StorageValidator
    {
        public abstract StorageValidatorResult Validate(IFormFile formFile);
    }
}
