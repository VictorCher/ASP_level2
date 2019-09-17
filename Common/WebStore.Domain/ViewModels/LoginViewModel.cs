using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    /// <summary>Модель входа пользователя на сайт</summary>
    public class LoginViewModel
    {
        /// <summary>Имя пользователя</summary>
        [Display(Name = "Имя пользвоателя"), MaxLength(256, ErrorMessage = "Максимальная длина 256 символов")]
        public string UserName { get; set; }

        /// <summary>Пароль</summary>
        [Display(Name = "Пароль"), Required, DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>Запомнить ли?</summary>
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        /// <summary>Куда перенаправить в случае успеха?</summary>
        public string ReturnUrl { get; set; }
    }
}
