using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _Logger;

        public HomeController(ILogger<HomeController> Logger) => _Logger = Logger;

        //[ActionFilterAsync]
        public IActionResult Index() => View();

        public IActionResult ThrowException() => throw new ApplicationException("Отладочное исключение");

        public IActionResult ContactUs() => View();

        public IActionResult Blog()
        {
            _Logger.LogInformation("Запрос блога");
            return View();
        }

        public IActionResult BlogSingle() => View();

        public IActionResult NotFoundPage()
        {
            _Logger.LogWarning("Запрос страницы 404");
            return View();
        }

        public IActionResult ErrorStatus(string id)
        {
            _Logger.LogWarning("Запрос статусного кода ошибки {0}", id);

            switch (id)
            {
                default: return Content($"Статусный код ошибки {id}");
                case "404": return RedirectToAction(nameof(NotFoundPage));
            }
        }
    }
}