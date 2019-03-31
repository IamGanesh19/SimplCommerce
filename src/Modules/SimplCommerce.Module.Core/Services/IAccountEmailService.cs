using System.Threading.Tasks;
using SimplCommerce.Module.Core.Models;

namespace SimplCommerce.Module.Core.Services
{
    public interface IAccountEmailService
    {
        Task SendWelcomeEmailToUser(User user, string confirmEmailURL);

        Task SendForgotPasswordEmailToUser(User user, string resetPasswordURL);
    }
}
