namespace SimplCommerce.Module.Shipments.Models
{
    public enum ShipmentStatus
    {
        Shipped,
        InTransit,
        OutForDelivery,
        Delivered,
        AttemptFail,
        Exception
    }
}
