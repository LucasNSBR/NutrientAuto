using NutrientAuto.Shared.ValueObjects;
using System.IO;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageService
{
    public interface IStorageService
    {
        Task<bool> CheckFileExistsAsync(string name);
        Task<Image> FindFileByNameAsync(string name);
        Task<StorageResult> UploadFileToStorageAsync(string base64, string fileName);
        Task<StorageResult> UploadFileToStorageAsync(MemoryStream stream, string fileName);
    }
}
