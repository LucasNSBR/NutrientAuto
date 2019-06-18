using System;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageValidators
{
    public class StorageValidatorError
    {
        public string ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public DateTime DateCreated { get; private set; }

        public StorageValidatorError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            DateCreated = DateTime.Now;
        }
    }
}
