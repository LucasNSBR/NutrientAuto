namespace NutrientAuto.Shared.ValueObjects
{
    public class CrnNumber : ValueObject<CrnNumber>
    {
        public string Number { get; private set; }
        public CrnRegion Region { get; private set; }

        public CrnNumber(string number, CrnRegion region)
        {
            Number = number;
            Region = region;
        }

        public override string ToString()
        {
            return $"{Number} - {Region.ToString()}";
        }
    }
}
