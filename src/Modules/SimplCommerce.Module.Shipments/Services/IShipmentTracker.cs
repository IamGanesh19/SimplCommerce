using SimplCommerce.Module.Shipments.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimplCommerce.Module.Shipments.Services
{
    public interface IShipmentTracker
    {
        void CreateTracking(string courier, string trackingNumber);

        IList<Courier> GetCouriers();

        ShipmentStatus GetLastknownStatus(string courier, string trackingNumber);
    }
}
