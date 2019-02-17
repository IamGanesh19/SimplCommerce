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

        [HttpPost]
        public async Task<IActionResult> Charge(string nonce)
        {
            var curentUser = await _workContext.GetCurrentUser();
            var cart = await _cartService.GetActiveCartDetails(curentUser.Id);

            var orderCreateResult = await _orderService.CreateOrder(cart.Id, PaymentProviderHelper.CashfreeProviderId, 0, OrderStatus.PendingPayment);

            if (!orderCreateResult.Success)
            {
                return BadRequest(orderCreateResult.Error);
            }

            var order = orderCreateResult.Value;
            var zeroDecimalOrderAmount = order.OrderTotal;
            if (!CurrencyHelper.IsZeroDecimalCurrencies())
            {
                zeroDecimalOrderAmount = zeroDecimalOrderAmount * 100;
            }

            var regionInfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            var payment = new Payment()
            {
                OrderId = order.Id,
                Amount = order.OrderTotal,
                PaymentMethod = PaymentProviderHelper.CashfreeProviderId,
                CreatedOn = DateTimeOffset.UtcNow
            };

            return Ok("Sucess");

            //var result = gateway.Transaction.Sale(request);
            //if (result.IsSuccess())
            //{
            //    var transaction = result.Target;

            //    payment.GatewayTransactionId = transaction.Id;
            //    payment.Status = PaymentStatus.Succeeded;
            //    order.OrderStatus = OrderStatus.PaymentReceived;
            //    _paymentRepository.Add(payment);
            //    await _paymentRepository.SaveChangesAsync();

            //    return Ok(transaction.Id);
            //}
            //else
            //{
            //    string errorMessages = "";
            //    foreach (var error in result.Errors.DeepAll())
            //    {
            //        errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
            //    }

            //    return BadRequest(errorMessages);
            //}
        }
    }
}
