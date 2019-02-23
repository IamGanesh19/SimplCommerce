using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Infrastructure.Helpers;
using SimplCommerce.Infrastructure.Web;
using SimplCommerce.Module.Core.Extensions;
using SimplCommerce.Module.Payments.Models;
using SimplCommerce.Module.PaymentCashfree.Areas.PaymentCashfree.ViewModels;
using SimplCommerce.Module.PaymentCashfree.Models;
using SimplCommerce.Module.ShoppingCart.Services;
using System;

namespace SimplCommerce.Module.PaymentCashfree.Areas.PaymentCashfree.Components
{
    public class CashfreeLandingViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly IWorkContext _workContext;
        private readonly IRepositoryWithTypedId<PaymentProvider, string> _paymentProviderRepository;
        

        public CashfreeLandingViewComponent(ICartService cartService, IWorkContext workContext, IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository)
        {
            _cartService = cartService;
            _workContext = workContext;
            _paymentProviderRepository = paymentProviderRepository;            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cashfreeProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelper.CashfreeProviderId);
            var cashfreeSetting = JsonConvert.DeserializeObject<CashfreeConfigForm>(cashfreeProvider.AdditionalSettings);
            var currentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetActiveCartDetails(currentUser.Id);
            
            currentUser.PhoneNumber = "8903440712";
            var paymentModes = "";

            int zerodecimalamount = 0;
            if (!CurrencyHelper.IsZeroDecimalCurrencies())
            {
                zerodecimalamount = (int)cart.OrderTotal;
            }

            var orderId = DateTime.Today.ToString("dM") + "-" + cart.Id;

            var message = "appId=" + cashfreeSetting.AppId + "&orderId=" + orderId + "&orderAmount=" + zerodecimalamount + "&returnUrl=" + cashfreeSetting.ReturnURL + "&paymentModes=" + paymentModes;
            var paymentToken = PaymentProviderHelper.GetToken(message, cashfreeSetting.SecretKey);
            var model = new CashfreeCheckoutForm
            {
                AppId = cashfreeSetting.AppId,
                PaymentToken = paymentToken,
                OrderId = orderId,
                OrderAmount = zerodecimalamount,
                CustomerName = currentUser.FullName,
                CustomerEmail = currentUser.Email,
                CustomerPhone = currentUser.PhoneNumber,
                Mode = cashfreeSetting.IsSandbox ? "TEST" : "PROD",
                ReturnURL = cashfreeSetting.ReturnURL,
                NotifyURL = cashfreeSetting.NotifyURL
            };            

            return View(this.GetViewPath(), model);
        }        
    }
}
