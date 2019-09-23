using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Domain.Entities.User.RoleAdmin)]
    public class HomeController : Controller
    {
        private readonly IProductData _ProductData;

        public HomeController(IProductData ProductData)
        {
            _ProductData = ProductData;
        }

        public IActionResult Index() => View();

        public IActionResult ProductList()
        {
            var products_dto = _ProductData.GetProducts(new ProductFilter());
            return View(products_dto);
        }
    }
}