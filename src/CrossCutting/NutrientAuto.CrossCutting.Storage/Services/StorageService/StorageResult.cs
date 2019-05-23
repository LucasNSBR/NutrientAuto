using System;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageService
{
    public class StorageResult
    {
        public bool Success { get; }
        public string FileUrl { get; }
        public string FileName { get; }
        public Exception Exception { get; }

        private StorageResult(string fileUrl, string fileName)
        {
            Success = true;
            FileUrl = fileUrl;
            FileName = fileName;
        }

        private StorageResult(Exception exception)
        {
            Success = false;
            Exception = exception;
        }

        public static StorageResult Ok(string fileUrl, string fileName)
        {
            return new StorageResult(fileUrl, fileName);
        }

        public static StorageResult Failure(Exception exception)
        {
            return new StorageResult(exception);
        }
    }
}
