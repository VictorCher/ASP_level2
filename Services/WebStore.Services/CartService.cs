using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Implementations
{
    public class CartService : ICartService
    {
        private readonly IProductData _ProductData;
        private readonly ICartStore _CartStore;

        public CartService(IProductData ProductData, ICartStore CartStore)
        {
            _ProductData = ProductData;
            _CartStore = CartStore;
        }

        public void DecrementFromCart(int id)
        {
            var cart = _CartStore.Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            if (item.Quantity > 0) item.Quantity--;
            if (item.Quantity == 0) cart.Items.Remove(item);

            _CartStore.Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = _CartStore.Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            cart.Items.Remove(item);

            _CartStore.Cart = cart;
        }

        public void RemoveAll()
        {
            var cart = _CartStore.Cart;
            cart.Items.Clear();
            _CartStore.Cart = cart;
        }

        public void AddToCart(int id)
        {
            var cart = _CartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);

            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                item.Quantity++;

            _CartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _ProductData.GetProducts(new ProductFilter
            {
                Ids = _CartStore.Cart.Items.Select(item => item.ProductId).ToList()
            });

            var products_view_models = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Brand = p.Brand?.Name
            });

            return new CartViewModel
            {
                Items = _CartStore.Cart.Items.ToDictionary(
                    x => products_view_models.First(p => p.Id == x.ProductId),
                    x => x.Quantity)
            };

        }
    }
}
