using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using NutrientAuto.CrossCutting.Storage.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageService
{
    public class StorageService : IStorageService
    {
        private readonly string _accountName;
        private readonly string _accountKey;

        public StorageService(IOptions<AccountOptions> options)
        {
            _accountName = options.Value.AccountName;
            _accountKey = options.Value.AccountKey;
        }

        private CloudBlobContainer GetContainer(string containerName)
        {
            StorageCredentials credentials = new StorageCredentials(_accountName, _accountKey);

            CloudBlobContainer container = new CloudStorageAccount(credentials, true)
                .CreateCloudBlobClient()
                .GetContainerReference(containerName);

            return container;
        }

        public async Task<bool> CheckFileExistsAsync(string containerName, string fileName)
        {
            CloudBlockBlob blob = GetContainer(containerName)
                .GetBlockBlobReference(fileName);

            return await blob.ExistsAsync();
        }
        
        public async Task<StorageResult> UploadFileToStorageAsync(string containerName, MemoryStream stream, string fileName, Dictionary<string, string> metadata = null)
        {
            CloudBlockBlob blob = GetContainer(containerName)
                .GetBlockBlobReference(fileName);

            SetMetadata(blob, metadata);

            try
            {
                await blob.UploadFromStreamAsync(stream);

                return StorageResult.Ok(blob.Uri.AbsoluteUri.ToString(), fileName);
            }
            catch (Exception ex)
            {
                return StorageResult.Failure(ex);
            }
        }

        private void SetMetadata(CloudBlockBlob blob, Dictionary<string, string> metadata)
        {
            if (metadata != null)
            {
                foreach (KeyValuePair<string, string> keyValue in metadata)
                {
                    blob.Metadata.Add(keyValue);
                }
            }
        }
    }
}
