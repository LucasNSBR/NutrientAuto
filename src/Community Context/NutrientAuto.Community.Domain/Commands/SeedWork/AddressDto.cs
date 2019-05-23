namespace NutrientAuto.Community.Domain.Commands.SeedWork
{
    public class AddressDto
    {
        public int Number { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string Complementation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Cep { get; set; }
    }
}
