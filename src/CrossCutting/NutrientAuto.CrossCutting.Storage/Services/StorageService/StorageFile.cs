namespace NutrientAuto.CrossCutting.Storage.Services.StorageService
{
    public class StorageFile
    {
        public string Name { get; }
        public string UrlPath { get; }

        public StorageFile(string name, string urlPath)
        {
            Name = name;
            UrlPath = urlPath;
        }
    }
}
