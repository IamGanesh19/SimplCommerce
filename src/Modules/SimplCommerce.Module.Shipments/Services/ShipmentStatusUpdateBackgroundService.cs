using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Orders.Models;
using SimplCommerce.Module.Shipments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimplCommerce.Module.Shipments.Services
{
    public class ShipmentStatusUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public ShipmentStatusUpdateBackgroundService(IServiceProvider serviceProvider, ILogger<ShipmentStatusUpdateBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ShipmentStatusUpdateBackgroundService is starting.");
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("ShipmentStatusUpdateBackgroundService is working.");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var shipmentRepository = scope.ServiceProvider.GetRequiredService<IRepository<Shipment>>();
                    var shipmentTracker = scope.ServiceProvider.GetRequiredService<IShipmentTracker>();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await UpdateShipmentStatus(shipmentRepository, shipmentTracker, mediator, stoppingToken);
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }

            _logger.LogInformation("ShipmentStatusUpdateBackgroundService is stopping.");
        }

        private async Task UpdateShipmentStatus(IRepository<Shipment> shipmentRepository, IShipmentTracker shipmentTracker, IMediator mediator, CancellationToken stoppingToken)
        {
            var shipmentsToTrack = shipmentRepository.Query().Where(x =>
                x.Order.OrderStatus == OrderStatus.Shipped
                && !string.IsNullOrWhiteSpace(x.TrackingNumber)
                && !string.IsNullOrWhiteSpace(x.Courier));

            foreach (var shipment in shipmentsToTrack)
            {
                var shipmentStatus = shipmentTracker.GetLastknownStatus(shipment.Courier, shipment.TrackingNumber);
                if (!shipmentStatus.ToString().Equals(shipment.Status, StringComparison.InvariantCultureIgnoreCase))
                {
                    shipment.Status = shipmentStatus.ToString();
                    // TO DO Mediator Publish if needed
                }
            }

            await shipmentRepository.SaveChangesAsync();
        }
    }
}
