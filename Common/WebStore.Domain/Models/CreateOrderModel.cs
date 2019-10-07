using System.Collections.Generic;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.DTO.Product;
using WebStore.ViewModels;

namespace WebStore.Domain.Models
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
