using System;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageValidators
{
    public class StorageValidatorError
    {
        public string ErrorCode { get; private set; }
        public string Description { get; private set; }
        public DateTime DateCreated { get; private set; }

        public StorageValidatorError(string errorCode, string description)
        {
            ErrorCode = errorCode;
            Description = description;
            DateCreated = DateTime.Now;
        }
    }
}
