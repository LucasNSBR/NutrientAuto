namespace NutrientAuto.CrossCutting.EmailService.Configuration
{
    public class EmailServiceOptions
    {
        public string SendGridKey { get; set; }
        public string SenderAddress { get; set; }
        public string SenderName { get; set; }
    }
}
