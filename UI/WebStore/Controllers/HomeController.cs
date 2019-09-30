using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _Logger;

        public HomeController(ILogger<HomeController> Logger) { _Logger = Logger; }

        //[ActionFilterAsync]
        public IActionResult Index() => View();

        public IActionResult ThrowException() => throw new ApplicationException("Отладочное исключение");

        public IActionResult ContactUs() => View();

        public IActionResult Checkout() => View();

        public IActionResult Blog()
        {
            _Logger.LogInformation("Запрос блога");
            return View();
        }

        public IActionResult BlogSingle() => View();

        public IActionResult Error404()
        {
            _Logger.LogWarning("Запрос страницы 404");
            return View();
        }
    }
}