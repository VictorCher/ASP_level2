using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewModels
{
    /// <summary>Модель-представления регистрации пользователя в системе</summary>
    public class RegistrerUserViewModel
    {
        /// <summary>Имя пользвоателя</summary>
        [Display(Name = "Имя пользвоателя"), Required, MaxLength(256, ErrorMessage = "Максимальная длина 256 символов")]
        [Remote("IsUserNameFree", "Account", ErrorMessage = "Пользователь с таким именем уже существует")]
        public string UserName { get; set; }

        /// <summary>Пароль</summary>
        [Display(Name = "Пароль"), Required, DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>Подтверждение пароля</summary>
        [Display(Name = "Подтверждение пароля"), Required, DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
