namespace NutrientAuto.Shared.ValueObjects
{
    public class Image : ValueObject<Image>
    {
        public string UrlPath { get; private set; }
        public string ImageName { get; private set; }

        protected Image()
        {
        }

        public Image(string urlPath, string imageName)
        {
            UrlPath = urlPath;
            ImageName = imageName;
        }

        public static Image Default()
        {
            return new Image(null, null);
        }

        public override string ToString()
        {
            return ImageName;
        }
    }
}
