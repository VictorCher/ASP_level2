using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO.Order;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Models;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _CartService;
        private readonly IOrderService _OrderService;

        public CartController(ICartService CartService, IOrderService OrderService)
        {
            _CartService = CartService;
            _OrderService = OrderService;
        }

        public IActionResult Details()
        {
            var model = new DetailsViewModel
            {
                CartViewModel = _CartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };
            return View(model);
        }

        public IActionResult DecrementFromCart(int id)
        {
            _CartService.DecrementFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveFromCart(int id)
        {
            _CartService.RemoveFromCart(id);
            return RedirectToAction("Details");
        }

        public IActionResult RemoveAll()
        {
            _CartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int id)
        {
            _CartService.AddToCart(id);
            return RedirectToAction("Details");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CheckOut(OrderViewModel model)
        {
            if (!ModelState.IsValid)
                return View(nameof(Details), new DetailsViewModel
                {
                    CartViewModel = _CartService.TransformCart(),
                    OrderViewModel = model
                });

            var create_order_model = new CreateOrderModel
            {
                OrderViewModel = model,
                OrderItems = _CartService.TransformCart().Items
                   .Select(item => new OrderItemDTO
                    {
                        Id = item.Key.Id,
                        Price = item.Key.Price,
                        Quantity = item.Value
                    })
                   .ToList()
            };

            var order = _OrderService.CreateOrder(create_order_model, User.Identity.Name);

            _CartService.RemoveAll();

            return RedirectToAction("OrderConfirmed", new { id = order.Id });
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}