using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities;

namespace WebStore.Services.Map
{
    public static class OrderItemPapper
    {
        public static OrderItemDTO ToDTO(this OrderItem item) => item is null ? null : new OrderItemDTO
        {
            Id = item.Id,
            Price = item.Price,
            Quantity = item.Quantity
        };

        public static OrderItem FromDTO(this OrderItemDTO item) => item is null ? null : new OrderItem
        {
            Id = item.Id,
            Price = item.Price,
            Quantity = item.Quantity
        };
    }
}