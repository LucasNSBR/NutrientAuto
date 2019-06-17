namespace NutrientAuto.CrossCutting.IoC.Configuration.Service
{
    public class StorageOptions
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }

        public string PostImageContainerName { get; set; }
        public string ProfileImageContainerName { get; set; }
        public string MeasureImageContainerName { get; set; }
    }
}
