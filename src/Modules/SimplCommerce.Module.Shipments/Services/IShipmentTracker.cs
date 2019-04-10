using SimplCommerce.Module.Shipments.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimplCommerce.Module.Shipments.Services
{
    public interface IShipmentTracker
    {
        Task CreateTracking(string courier, string trackingNumber);

        Task<IList<Courier>> GetCouriers();
    }
}
