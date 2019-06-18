using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageService
{
    public interface IStorageService
    {
        Task<bool> CheckFileExistsAsync(string containerName, string fileName);
        Task<StorageFile> FindFileAsync(string containerName, string fileName);
        Task<StorageResult> UploadFileToStorageAsync(string containerName, MemoryStream stream, string fileName, Dictionary<string, string> metadata = null);
    }
}
