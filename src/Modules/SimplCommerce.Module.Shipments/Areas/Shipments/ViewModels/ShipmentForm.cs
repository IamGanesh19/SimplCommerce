using System.Collections.Generic;
using SimplCommerce.Module.Shipments.Models;
using SimplCommerce.Module.Shipments.Services;

namespace SimplCommerce.Module.Shipments.Areas.Shipments.ViewModels
{
    public class ShipmentForm
    {
        public long OrderId { get; set; }

        public long WarehouseId { get; set; }

        public string TrackingNumber { get; set; }

        public string Courier { get; set; }

        public ShipmentStatus Status { get; set; }

        public IList<ShipmentItemVm> Items { get; set; } = new List<ShipmentItemVm>();
    }
}
