using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Core.Extensions;
using SimplCommerce.Module.Shipments.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SimplCommerce.Module.Shipments.Areas.Shipments.Controllers
{
    [Area("Shipments")]
    [Authorize]
    public class TrackController : Controller
    {
        private readonly IRepository<Shipment> _shipmentRepository;
        private readonly IWorkContext _workContext;

        public TrackController(IRepository<Shipment> shipmentRepository, IWorkContext workContext)
        {
            _shipmentRepository = shipmentRepository;
            _workContext = workContext;
        }

        [HttpGet("user/orders/{orderId}/track")]
        public async Task<IActionResult> TrackShipment(long orderId)
        {
            var user = await _workContext.GetCurrentUser();

            var shipment = _shipmentRepository
                .Query()
                .FirstOrDefault(x => x.OrderId == orderId);

            if (shipment == null || string.IsNullOrWhiteSpace(shipment.TrackingNumber))
            {
                return NotFound();
            }

            //return View(shipment.TrackingNumber);
            return Redirect("https://asritasilks.aftership.com/"+ shipment.TrackingNumber);
        }
    }
}
