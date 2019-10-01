using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.Entities;

namespace WebStore.Services.Map
{
    public static class OrderMapper
    {
        public static OrderDTO ToDTO(this Order order) => order is null ? null : new OrderDTO
        {
            Id = order.Id,
            Name = order.Name,
            Address = order.Address,
            Date = order.Date,
            Phone = order.Phone,
            OrderItems = order.OrderItems.Select(item => item.ToDTO()).ToArray()
        };

        public static Order FromDTO(this OrderDTO order) => order is null ? null : new Order
        {
            Id = order.Id,
            Name = order.Name,
            Address = order.Address,
            Date = order.Date,
            Phone = order.Phone,
            OrderItems = order.OrderItems.Select(item => item.FromDTO()).ToArray()
        };
    }
}
