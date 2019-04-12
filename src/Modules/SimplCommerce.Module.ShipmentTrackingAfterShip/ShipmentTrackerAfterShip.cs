using AftershipAPI;
using Microsoft.Extensions.Configuration;
using SimplCommerce.Module.Shipments.Models;
using SimplCommerce.Module.Shipments.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimplCommerce.Module.ShipmentTrackingAfterShip
{
    public class ShipmentTrackerAfterShip : IShipmentTracker
    {
        private readonly string _apiKey;
        private ConnectionAPI _connection;

        public ShipmentTrackerAfterShip(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("Aftership.ApiKey");
            Contract.Requires(string.IsNullOrWhiteSpace(_apiKey));

            _connection = new ConnectionAPI(_apiKey);
        }

        public void CreateTracking(string courier, string trackingNumber)
        {
            Contract.Requires(string.IsNullOrWhiteSpace(courier));
            Contract.Requires(string.IsNullOrWhiteSpace(trackingNumber));

            var tracking = new Tracking(trackingNumber);
            tracking.Slug = courier;

            _connection.CreateTracking(tracking);
        }

        public IList<Shipments.Models.Courier> GetCouriers()
        {
            var couriers = new List<Shipments.Models.Courier>();

            var aftershipCouriers = _connection.GetCouriers();
            foreach (var aftershipCourier in aftershipCouriers)
            {
                couriers.Add(new Shipments.Models.Courier() { Id = aftershipCourier.Slug, Name = aftershipCourier.Name });
            }

            return couriers;
        }

        public ShipmentStatus GetLastknownStatus(string courier, string trackingNumber)
        {
            var tracking = new Tracking(trackingNumber);
            tracking.Slug = courier;

            var lastCheckPoint = _connection.GetLastCheckpoint(tracking).Tag;
            switch (lastCheckPoint)
            {
                case "InTransit":
                    return ShipmentStatus.InTransit;
                case "OutForDelivery":
                    return ShipmentStatus.OutForDelivery;
                case "Delivered":
                    return ShipmentStatus.Delivered;
                case "Exception":
                    return ShipmentStatus.Exception;
                case "AttemptFail":
                    return ShipmentStatus.AttemptFail;
                default:
                    return ShipmentStatus.Shipped;
            }
        }
    }
}
