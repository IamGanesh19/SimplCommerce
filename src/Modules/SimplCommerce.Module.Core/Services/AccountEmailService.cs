using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using SimplCommerce.Infrastructure.Web;
using SimplCommerce.Module.Core.Models;
using System.Threading.Tasks;

namespace SimplCommerce.Module.Core.Services
{
    public class AccountEmailService : IAccountEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IRazorViewRenderer _viewRender;
        private readonly IRazorViewEngine _viewEngine;
        private string theme;

        public AccountEmailService(IEmailSender emailSender, IRazorViewRenderer viewRender, IConfiguration config, IRazorViewEngine viewEngine)
        {
            _emailSender = emailSender;
            _viewRender = viewRender;
            _viewEngine = viewEngine;
            theme = config["Theme"];
        }

        public async Task SendWelcomeEmailToUser(User user, string confirmEmailURL)
        {
            var viewPath = "/Areas/Core/Views/EmailTemplates/Welcome.cshtml";
            if (!string.IsNullOrWhiteSpace(theme) && !string.Equals(theme, "Generic", System.StringComparison.InvariantCultureIgnoreCase))
            {
                var themeViewPath = $"/Themes/{theme}{viewPath}";
                var result = _viewEngine.GetView("", themeViewPath, isMainPage: false);
                if (result.Success)
                {
                    viewPath = themeViewPath;
                }
            }
            var emailBody = await _viewRender.RenderViewToStringAsync(viewPath, confirmEmailURL);
            var emailSubject = $"Welcome! Confirm your email address";
            await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody, true);
        }

        public async Task SendForgotPasswordEmailToUser(User user, string resetPasswordURL)
        {
            var viewPath = "/Areas/Core/Views/EmailTemplates/ForgotPassword.cshtml";
            if (!string.IsNullOrWhiteSpace(theme) && !string.Equals(theme, "Generic", System.StringComparison.InvariantCultureIgnoreCase))
            {
                var themeViewPath = $"/Themes/{theme}{viewPath}";
                var result = _viewEngine.GetView("", themeViewPath, isMainPage: false);
                if (result.Success)
                {
                    viewPath = themeViewPath;
                }
            }
            var emailBody = await _viewRender.RenderViewToStringAsync(viewPath, resetPasswordURL);
            var emailSubject = $"Reset Password";
            await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody, true);
        }
    }
}
