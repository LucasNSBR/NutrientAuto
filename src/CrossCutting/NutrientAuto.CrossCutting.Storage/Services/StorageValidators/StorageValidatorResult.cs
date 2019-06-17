using System.Collections.Generic;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageValidators
{
    public class StorageValidatorResult
    {
        public bool Success { get; }

        private readonly List<StorageValidatorError> _errors;
        public IReadOnlyList<StorageValidatorError> Errors => _errors;

        public StorageValidatorResult(bool success, List<StorageValidatorError> errors = null)
        {
            Success = success;

            _errors = errors ?? new List<StorageValidatorError>();
        }
    }
}
