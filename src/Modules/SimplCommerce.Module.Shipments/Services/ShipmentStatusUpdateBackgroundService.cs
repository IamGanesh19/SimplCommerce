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
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await UpdateOrderStatus(shipmentRepository, mediator, stoppingToken);
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }

            _logger.LogInformation("ShipmentStatusUpdateBackgroundService is stopping.");
        }

        private async Task UpdateOrderStatus(IRepository<Shipment> shipmentRepository, IMediator mediator, CancellationToken stoppingToken)
        {
            var shipmentsToTrack = shipmentRepository.Query().Where(x =>
                x.Order.OrderStatus == OrderStatus.Shipped
                && !string.IsNullOrWhiteSpace(x.TrackingNumber));

            //foreach (var order in shipmentsToTrack)
            //{
            //    orderService.CancelOrder(order);
            //    var orderStatusChanged = new OrderChanged
            //    {
            //        OrderId = order.Id,
            //        OldStatus = OrderStatus.PendingPayment,
            //        NewStatus = OrderStatus.Canceled,
            //        UserId = SystemUserId,
            //        Order = order,
            //        Note = "System canceled"
            //    };

            //    await mediator.Publish(orderStatusChanged, stoppingToken);
            //}

            //await orderRepository.SaveChangesAsync();
        }
    }
}
