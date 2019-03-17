using System.Threading.Tasks;

namespace SimplCommerce.Module.Core.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, bool isHtml = false);

        Task SendEmailAsync(string from, string to, string subject, string message, bool isHtml = false);
    }
}
