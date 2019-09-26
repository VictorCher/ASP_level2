using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Models;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        private readonly ILogger<OrdersClient> _Logger;

        public OrdersClient(IConfiguration configuration, ILogger<OrdersClient> Logger) 
            : base(configuration, "api/orders") => 
            _Logger = Logger;

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
            //_Logger.LogInformation("Создание нового заказа для {0}", UserName);

            //using (_Logger.BeginScope("Создание нового заказа для {0}", UserName))
            {
                var response = Post($"{_ServiceAddress}/{UserName}", OrderModel);
                _Logger.LogInformation("Заказ для {0} успешно создан", UserName);
                return response.Content.ReadAsAsync<OrderDTO>().Result;
            }
        }
    }
}
