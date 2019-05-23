using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using NutrientAuto.CrossCutting.Storage.Configuration;
using NutrientAuto.Shared.ValueObjects;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageService
{
    public class StorageService : IStorageService
    {
        private readonly string _accountName;
        private readonly string _accountKey;
        private readonly string _containerName;

        public StorageService(IOptions<StorageOptions> options)
        {
            _accountName = options.Value.AccountName;
            _accountKey = options.Value.AccountKey;
            _containerName = options.Value.ContainerName;
        }

        private CloudBlobContainer GetContainer()
        {
            StorageCredentials credentials = new StorageCredentials(_accountName, _accountKey);

            CloudBlobContainer container = new CloudStorageAccount(credentials, true)
                .CreateCloudBlobClient()
                .GetContainerReference(_containerName);

            return container;
        }

        public async Task<bool> CheckFileExistsAsync(string name)
        {
            CloudBlockBlob block = GetContainer()
                .GetBlockBlobReference(name);

            return await block.ExistsAsync();
        }

        public async Task<Image> FindFileByNameAsync(string name)
        {
            CloudBlockBlob block = GetContainer()
                .GetBlockBlobReference(name);

            bool exists = await block.ExistsAsync();
            if (exists)
                return new Image(block.Uri.AbsoluteUri.ToString(), block.Name);

            return null;
        }

        public async Task<StorageResult> UploadFileToStorageAsync(string base64, string fileName)
        {
            CloudBlockBlob block = GetContainer()
                .GetBlockBlobReference(fileName);

            try
            {
                Stream file = new MemoryStream(Convert.FromBase64String(base64));
                await block.UploadFromStreamAsync(file);

                return StorageResult.Ok(block.Uri.AbsoluteUri.ToString(), fileName);
            }
            catch (Exception ex)
            {
                return StorageResult.Failure(ex);
            }
        }

        public async Task<StorageResult> UploadFileToStorageAsync(MemoryStream stream, string fileName)
        {
            CloudBlockBlob block = GetContainer()
                .GetBlockBlobReference(fileName);

            try
            {
                await block.UploadFromStreamAsync(stream);

                return StorageResult.Ok(block.Uri.AbsoluteUri.ToString(), fileName);
            }
            catch (Exception ex)
            {
                return StorageResult.Failure(ex);
            }
        }
    }
}
