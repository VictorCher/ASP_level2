using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components
{
    [ViewComponent(Name = "UserInfo")] // <-- Имя компоненту можно дать тут
    public class UserInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() => User.Identity.IsAuthenticated 
            ? View("UserInfoView") 
            : View();
    }
}
