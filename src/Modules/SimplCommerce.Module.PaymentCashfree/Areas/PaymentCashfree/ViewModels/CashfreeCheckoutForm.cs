namespace SimplCommerce.Module.PaymentCashfree.Areas.PaymentCashfree.ViewModels
{
    public class CashfreeCheckoutForm
    {
        public string PublicKey { get; set; }

        public int Amount { get; set; }

        public string ISOCurrencyCode { get; set; }
    }
}
