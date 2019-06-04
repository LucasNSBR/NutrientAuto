using Microsoft.Extensions.Options;
using NutrientAuto.CrossCutting.Storage.Configuration;
using NutrientAuto.Shared.ValueObjects;

namespace NutrientAuto.CrossCutting.Storage.Services.StorageDefinitions
{
    public class StorageDefinitions : IStorageDefinitions
    {
        private readonly IOptions<StorageOptions> _storageOptions;

        public StorageDefinitions(IOptions<StorageOptions> storageOptions)
        {
            _storageOptions = storageOptions;
        }

        public Image GetDefaultProfileAvatarImage()
        {
            return new Image(_storageOptions.Value.DefaultAvatarImageName, _storageOptions.Value.DefaultAvatarImageUrl);
        }
    }
}
