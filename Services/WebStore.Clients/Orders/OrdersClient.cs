using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Order;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, "api/orders") { }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName)
        {
            return Get<List<OrderDTO>>($"{_ServiceAddress}/user/{UserName}");
        }

        public OrderDTO GetOrderById(int id)
        {
            return Get<OrderDTO>($"{_ServiceAddress}/{id}");
        }

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var response = Post($"{_ServiceAddress}/{UserName}", OrderModel);
            return response.Content.ReadAsAsync<OrderDTO>().Result;
        }
    }
}
