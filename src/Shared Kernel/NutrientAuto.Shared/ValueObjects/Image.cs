namespace NutrientAuto.Shared.ValueObjects
{
    public class Image : ValueObject<Image>
    {
        public string UrlPath { get; private set; }
        public string Name { get; private set; }

        protected Image()
        {
        }

        public Image(string urlPath, string name)
        {
            UrlPath = urlPath;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
