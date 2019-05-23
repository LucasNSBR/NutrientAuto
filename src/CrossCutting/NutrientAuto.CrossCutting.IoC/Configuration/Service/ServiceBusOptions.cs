namespace NutrientAuto.CrossCutting.IoC.Configuration.Service
{
    public class ServiceBusOptions
    {
        public string HostAddress { get; set; }
        public string RabbitMqHostUser { get; set; }
        public string RabbitMqHostPassword { get; set; }
        public string RabbitMqQueueName { get; set; }
    }
}
