using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;

namespace WebStore.Services.Map
{
    public static class ProductMapper
    {
        public static ProductDTO ToDTO(this Product product) => product is null ? null : new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.ToDTO()
        };

        public static Product FromDTO(this ProductDTO product) => product is null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Order = product.Order,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Brand = product.Brand.FromDTO()
        };
    }
}
