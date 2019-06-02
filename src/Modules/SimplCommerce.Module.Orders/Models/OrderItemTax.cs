using SimplCommerce.Infrastructure.Models;
using SimplCommerce.Module.GSTIndia.Models;

namespace SimplCommerce.Module.Orders.Models
{
    public class OrderItemTax : EntityBase
    {
        public string TaxType { get; set; }

        public decimal Rate { get; set; }

        public decimal TaxAmount { get; set; }
    }
}
