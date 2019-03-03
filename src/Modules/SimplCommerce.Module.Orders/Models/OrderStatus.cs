using System.ComponentModel;

namespace SimplCommerce.Module.Orders.Models
{
    public enum OrderStatus
    {
        New = 1,

        [Description("Order On-Hold")]
        OnHold = 10,

        [Description("Payment Pending")]
        PendingPayment = 20,

        [Description("Order Received")]
        PaymentReceived = 30,

        [Description("Payment Failed")]
        PaymentFailed = 35,

        Invoiced = 40,

        Shipping = 50,

        Shipped = 60,

        Complete = 70,

        Canceled = 80,

        Refunded = 90,

        Closed = 100
    }
}
