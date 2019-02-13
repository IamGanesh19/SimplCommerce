using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Infrastructure.Helpers;
using SimplCommerce.Module.Core.Extensions;
using SimplCommerce.Module.Orders.Models;
using SimplCommerce.Module.Orders.Services;
using SimplCommerce.Module.Payments.Models;
using SimplCommerce.Module.PaymentCashfree.Areas.PaymentCashfree.ViewModels;
using SimplCommerce.Module.PaymentCashfree.Models;
using SimplCommerce.Module.ShoppingCart.Services;

namespace SimplCommerce.Module.PaymentCashfree.Areas.PaymentCashfree.Controllers
{
    [Area("PaymentCashfree")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CashfreeController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly IRepositoryWithTypedId<PaymentProvider, string> _paymentProviderRepository;
        private readonly IRepository<Payment> _paymentRepository;

        public CashfreeController(
            ICartService cartService,
            IOrderService orderService,
            IWorkContext workContext,
            IRepositoryWithTypedId<PaymentProvider, string> paymentProviderRepository,
            IRepository<Payment> paymentRepository)
        {
            _cartService = cartService;
            _orderService = orderService;
            _workContext = workContext;
            _paymentProviderRepository = paymentProviderRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<IActionResult> Charge(string CashfreeEmail, string CashfreeToken)
        {
            //var cashfreeProvider = await _paymentProviderRepository.Query().FirstOrDefaultAsync(x => x.Id == PaymentProviderHelper.CashfreeProviderId);
            //var cashfreeSetting = JsonConvert.DeserializeObject<CashfreeConfigForm>(cashfreeProvider.AdditionalSettings);
            ////var cashfreeChargeService = new ChargeService(cashfreeSetting.PrivateKey);
            //var currentUser = await _workContext.GetCurrentUser();
            //var cart = await _cartService.GetActiveCart(currentUser.Id).FirstOrDefaultAsync();

            //var orderCreationResult = await _orderService.CreateOrder(cart.Id, "Cashfree", 0, OrderStatus.PendingPayment);
            //if (!orderCreationResult.Success)
            //{
            //    TempData["Error"] = orderCreationResult.Error;
            //    return Redirect("~/checkout/payment");
            //}

            //var order = orderCreationResult.Value;
            //var zeroDecimalOrderAmount = order.OrderTotal;
            //if (!CurrencyHelper.IsZeroDecimalCurrencies())
            //{
            //    zeroDecimalOrderAmount = zeroDecimalOrderAmount * 100;
            //}

            //var regionInfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            //var payment = new Payment()
            //{
            //    OrderId = order.Id,
            //    Amount = order.OrderTotal,
            //    PaymentMethod = "Cashfree",
            //    CreatedOn = DateTimeOffset.UtcNow
            //};
            //try
            //{
            //    var charge = cashfreeChargeService.Create(new ChargeCreateOptions
            //    {
            //        Amount = (int)zeroDecimalOrderAmount,
            //        Description = "Sample Charge",
            //        Currency = regionInfo.ISOCurrencySymbol,
            //        SourceId = CashfreeToken
            //    });

            //    payment.GatewayTransactionId = charge.Id;
            //    payment.Status = PaymentStatus.Succeeded;
            //    order.OrderStatus = OrderStatus.PaymentReceived;
            //    _paymentRepository.Add(payment);
            //    await _paymentRepository.SaveChangesAsync();
            //    return Redirect("~/checkout/congratulation");
            //}
            //catch (CashfreeException ex)
            //{
            //    payment.Status = PaymentStatus.Failed;
            //    payment.FailureMessage = ex.CashfreeError.Message;
            //    order.OrderStatus = OrderStatus.PaymentFailed;

            //    _paymentRepository.Add(payment);
            //    await _paymentRepository.SaveChangesAsync();
            //    TempData["Error"] = ex.CashfreeError.Message;
            //    return Redirect("~/checkout/payment");
            //}
            return Redirect("~/checkout/congratulation");
        }
    }
}
