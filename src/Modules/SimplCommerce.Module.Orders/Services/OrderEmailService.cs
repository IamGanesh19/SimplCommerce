using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Configuration;
using SimplCommerce.Infrastructure.Web;
using SimplCommerce.Module.Core.Models;
using SimplCommerce.Module.Core.Services;
using SimplCommerce.Module.Orders.Models;

namespace SimplCommerce.Module.Orders.Services
{
    public class OrderEmailService : IOrderEmailService
    {
        private readonly IEmailSender _emailSender;
        private readonly IRazorViewRenderer _viewRender;
        private readonly IRazorViewEngine _viewEngine;
        private string theme;

        public OrderEmailService(IEmailSender emailSender, IRazorViewRenderer viewRender, IConfiguration config, IRazorViewEngine viewEngine)
        {
            _emailSender = emailSender;
            _viewRender = viewRender;
            _viewEngine = viewEngine;
            theme = config["Theme"];
        }

        public async Task SendEmailToUser(User user, Order order)
        {
            var viewPath = string.Empty;
            var emailSubject = string.Empty;
            switch (order.OrderStatus)
            {
                case OrderStatus.New:
                    viewPath = "/Areas/Orders/Views/EmailTemplates/OrderEmailToCustomer.cshtml";
                    emailSubject = $"Order Confirmation";
                    break;
                case OrderStatus.OnHold:
                    break;
                case OrderStatus.PendingPayment:
                    break;
                case OrderStatus.PaymentReceived:
                    viewPath = "/Areas/Orders/Views/EmailTemplates/OrderEmailToCustomer.cshtml";
                    emailSubject = $"Order Confirmation";
                    break;
                case OrderStatus.PaymentFailed:
                    break;
                case OrderStatus.Invoiced:
                    break;
                case OrderStatus.Shipping:
                    break;
                case OrderStatus.Shipped:
                    viewPath = "/Areas/Orders/Views/EmailTemplates/ShippedEmailToCustomer.cshtml";
                    emailSubject = $"Order Shipped";
                    break;
                case OrderStatus.Complete:
                    break;
                case OrderStatus.Canceled:
                    break;
                case OrderStatus.Refunded:
                    break;
                case OrderStatus.Closed:
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrWhiteSpace(viewPath) && !string.IsNullOrWhiteSpace(emailSubject))
            {
                if (!string.IsNullOrWhiteSpace(theme) && !string.Equals(theme, "Generic", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    var themeViewPath = $"/Themes/{theme}{viewPath}";
                    var result = _viewEngine.GetView("", themeViewPath, isMainPage: false);
                    if (result.Success)
                    {
                        viewPath = themeViewPath;
                    }
                }
            
                var emailBody = await _viewRender.RenderViewToStringAsync(viewPath, order);                
                await _emailSender.SendEmailAsync(user.Email, emailSubject, emailBody, true);
            }
        }
    }
}
