using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }

        public async Task<IActionResult> IsUserNameFree(string UserName) =>
            await _UserManager.FindByNameAsync(UserName) is null
                ? Json(true)
                : Json(false);

        public IActionResult Register() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrerUserViewModel model, [FromServices] ILogger<AccountController> Logger)
        {
            if (!ModelState.IsValid)
            {
                Logger.LogWarning("Ошибка модели регистрации нового пользователя");
                return View(model); // Если данные в форме некоректны, то на доработку
            }

            var new_user = new User         // Создаём нового пользователя
            {
                UserName = model.UserName
            };
            // Пытаемся зарегистрировать его в системе с указанным паролем

            using (Logger.BeginScope("Регистрация нового пользователя {0}", model.UserName))
            {
                Logger.LogInformation("Регистрация новго пользовтеля {0}", model.UserName);
                var creation_result = await _UserManager.CreateAsync(new_user, model.Password);
                if (creation_result.Succeeded) // Если получилось
                {
                    Logger.LogInformation("Пользователь {0} успешно зарегистрирован.", model.UserName);
                    await _SignInManager.SignInAsync(new_user, false); // То сразу логиним его на сайте

                    Logger.LogInformation("Пользователь {0} вошёл в систему.", model.UserName);
                    return RedirectToAction("Index", "Home"); // и отправляем на главную страницу
                }

                Logger.LogWarning("Ошибка регистрации пользователя {0}: {1}",
                    model.UserName,
                    string.Join(", ", creation_result.Errors.Select(e => e.Description)));

                foreach (var error in creation_result.Errors)         // Если что-то пошло не так...
                    ModelState.AddModelError("", error.Description);  // Все ошибки заносим в состояние модели
            }

            return View(model);                                   // И модель отправляем на доработку
        }

        public IActionResult Login() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, [FromServices] ILogger<AccountController> Logger)
        {
            if (!ModelState.IsValid) return View(login);

            var login_result = await _SignInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);

            if (login_result.Succeeded)
            {
                Logger.LogInformation("Пользователь {0} вошёл в систему", login.UserName);

                return Url.IsLocalUrl(login.ReturnUrl)
                    ? (IActionResult)Redirect(login.ReturnUrl)
                    : RedirectToAction("Index", "Home");
            }

            Logger.LogWarning("Ошибка входа пользователя {0} в систему", login.UserName);
            ModelState.AddModelError("", "Имя пользователя, или пароль неверны!");
            return View(login);
        }

        public async Task<IActionResult> Logout([FromServices] ILogger<AccountController> Logger)
        {
            var user_name = User.Identity.Name;

            await _SignInManager.SignOutAsync();

            Logger.LogInformation("Пользователь {0} вышел из системы");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}