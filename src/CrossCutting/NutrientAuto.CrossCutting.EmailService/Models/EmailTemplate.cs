using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace NutrientAuto.CrossCutting.EmailService.Models
{
    public class EmailTemplate
    {
        public EmailAddress To { get; private set; }
        public string TemplateId { get; private set; }

        private readonly Dictionary<string, string> _sendGridSubstitutions;
        public IReadOnlyDictionary<string, string> SendGridSubstitutions
        {
            get
            {
                return _sendGridSubstitutions;
            }
        }

        public EmailTemplate()
        {
            _sendGridSubstitutions = new Dictionary<string, string>();
        }

        public EmailTemplate SetTo(string to)
        {
            To = new EmailAddress(to);

            return this;
        }

        public EmailTemplate SetTemplate(string templateId)
        {
            TemplateId = templateId;

            return this;
        }

        public EmailTemplate AddSubstitution(string key, string value)
        {
            if (_sendGridSubstitutions.ContainsKey(key))
                _sendGridSubstitutions[key] = value;
            else
                _sendGridSubstitutions.Add(key, value);

            return this;
        }
    }
}