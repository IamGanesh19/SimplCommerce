using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimplCommerce.Infrastructure.Data;
using SimplCommerce.Module.Core.Extensions;
using SimplCommerce.Module.Core.Models;
using SimplCommerce.Module.Orders.Models;
using SimplCommerce.Module.Orders.Services;

namespace SimplCommerce.Module.Orders.Events
{
    public class OrderCreatedSendEmailHandler : INotificationHandler<OrderCreated>
    {
        private readonly IOrderEmailService _orderEmailService;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;

        public OrderCreatedSendEmailHandler(IRepository<User> userRepository, IOrderEmailService orderEmailService, IRepository<Order> orderRepository)
        {
            _userRepository = userRepository;
            _orderEmailService = orderEmailService;
            _orderRepository = orderRepository;
        }

        public async Task Handle(OrderCreated notification, CancellationToken cancellationToken)
        {
            // Send order received email
            var order = _orderRepository
                .Query()
                .Include(x => x.ShippingAddress).ThenInclude(x => x.District)
                .Include(x => x.ShippingAddress).ThenInclude(x => x.StateOrProvince)
                .Include(x => x.ShippingAddress).ThenInclude(x => x.Country)
                .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.ThumbnailImage)
                .Include(x => x.OrderItems).ThenInclude(x => x.Product).ThenInclude(x => x.OptionCombinations).ThenInclude(x => x.Option)
                .Include(x => x.Customer)
                .FirstOrDefault(x => x.Id == notification.OrderId);

            var user = _userRepository
                .Query()
                .FirstOrDefault(x => x.Id == order.CustomerId);
            
            await _orderEmailService.SendEmailToUser(user, order);
        }
    }
}
