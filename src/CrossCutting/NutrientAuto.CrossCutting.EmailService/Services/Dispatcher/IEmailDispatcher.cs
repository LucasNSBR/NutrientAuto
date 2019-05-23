using NutrientAuto.CrossCutting.EmailService.Models;
using System.Threading.Tasks;

namespace NutrientAuto.CrossCutting.EmailService.Services.Dispatcher
{
    public interface IEmailDispatcher
    {
        Task SendEmailAsync(EmailTemplate template);
        Task SendEmailAsync(string to, string subject, string body);
    }
}
