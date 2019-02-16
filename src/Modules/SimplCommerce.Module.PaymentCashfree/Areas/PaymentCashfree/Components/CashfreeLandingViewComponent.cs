﻿using System.Globalization;
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
using System.Security.Cryptography;
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
            decimal total = 93;
            currentUser.PhoneNumber = "8903440712";
            //var zeroDecimalAmount = cart.OrderTotal;
            //if(!CurrencyHelper.IsZeroDecimalCurrencies())
            //{
            //    zeroDecimalAmount = zeroDecimalAmount * 100;
            //}

            var regionInfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            var paymentModes = "";
            //var message = "appId=\"" + cashfreeSetting.AppId + "\"&orderId=" + cart.Id + "&orderAmount=" + cart.OrderTotal + "&returnUrl=\"" + cashfreeSetting.ReturnURL + "\"&paymentModes=\"" + paymentModes + "\"";
            var message = "appId=" + cashfreeSetting.AppId + "&orderId=" + cart.Id + "&orderAmount=" + total + "&returnUrl=" + cashfreeSetting.ReturnURL + "&paymentModes=" + paymentModes;
            var paymentToken = GetPaymentToken(message, cashfreeSetting.SecretKey);
            var model = new CashfreeCheckoutForm
            {
                AppId = cashfreeSetting.AppId,
                PaymentToken = paymentToken,
                OrderId = cart.Id,
                OrderAmount = cart.OrderTotal,
                CustomerName = currentUser.FullName,
                CustomerEmail = currentUser.Email,
                CustomerPhone = currentUser.PhoneNumber,
                Mode = cashfreeSetting.IsSandbox ? "TEST" : "PROD",
                ReturnURL = cashfreeSetting.ReturnURL,
                NotifyURL = cashfreeSetting.NotifyURL
            };            

            return View(this.GetViewPath(), model);
        }

        private string GetPaymentToken(string message, string secretKey)
        {
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secretKey);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
    }
}
