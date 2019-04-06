using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Shipments.Models;
using System;
using System.Collections.Generic;
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
            //_logger.LogInformation("ShipmentStatusUpdateBackgroundService is starting.");
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("ShipmentStatusUpdateBackgroundService is working.");
            //    using (var scope = _serviceProvider.CreateScope())
            //    {
            //        var orderRepository = scope.ServiceProvider.GetRequiredService<IRepository<Shipment>>();
            //        //var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
            //        //var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            //        //await CancelFailedPaymentOrders(orderRepository, orderService, mediator, stoppingToken);
            //    }

            //    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            //}

            //_logger.LogInformation("ShipmentStatusUpdateBackgroundService is stopping.");
        }
    }
}
