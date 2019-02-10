using System.ComponentModel.DataAnnotations;

namespace SimplCommerce.Module.PaymentCashfree.Areas.PaymentCashfree.ViewModels
{
    public class CashfreeConfigForm
    {
        [Required(ErrorMessage = "The {0} field is required.")]
        public string PublicKey { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public string PrivateKey { get; set; }
    }
}
