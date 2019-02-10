using Microsoft.EntityFrameworkCore;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Payments.Models;

namespace SimplCommerce.Module.PaymentCashfree.Data
{
    public class PaymentCashfreeCustomModelBuilder : ICustomModelBuilder
    {
        public void Build(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentProvider>().HasData(
                new PaymentProvider("Cashfree") { Name = "Cashfree", LandingViewComponentName = "CashfreeLanding", ConfigureUrl = "payments-Cashfree-config", IsEnabled = true, AdditionalSettings = "{\"PublicKey\": \"pk_test_6pRNASCoBOKtIshFeQd4XMUh\", \"PrivateKey\" : \"sk_test_BQokikJOvBiI2HlWgH4olfQ2\"}" }
            );
        }
    }
}
