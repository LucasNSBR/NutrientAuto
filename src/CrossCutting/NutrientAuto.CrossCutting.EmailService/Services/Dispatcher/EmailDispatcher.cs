using Microsoft.Extensions.Options;
using NutrientAuto.CrossCutting.EmailService.Configuration;
using NutrientAuto.CrossCutting.EmailService.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Linq;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.EmailService.Services.Dispatcher
{
    public class EmailDispatcher : IEmailDispatcher
    {
        private readonly string _sendGridKey;
        private readonly EmailAddress _senderAddress;

        public EmailDispatcher(IOptions<EmailServiceOptions> options)
        {
            _sendGridKey = options.Value.SendGridKey;

            _senderAddress = new EmailAddress(options.Value.SenderAddress, options.Value.SenderName);
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            SendGridMessage message = new SendGridMessage();

            message.SetFrom(_senderAddress);
            message.AddTo(to);
            message.SetSubject(subject);
            message.AddContent("text/plain", body);

            await Dispatch(message);
        }

        public async Task SendEmailAsync(EmailTemplate template)
        {
            SendGridMessage message = new SendGridMessage();

            message.SetFrom(_senderAddress);
            message.AddTo(template.To);
            message.SetTemplateId(template.TemplateId);
            message.AddSubstitutions(template.SendGridSubstitutions.ToDictionary(k => k.Key, v => v.Value));

            await Dispatch(message);
        }

        private async Task Dispatch(SendGridMessage message)
        {
            SendGridClient client = new SendGridClient(_sendGridKey);
            await client.SendEmailAsync(message);
        }
    }
}
