using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Models
{
    /// <summary>Сотрудник</summary>
    public class Employee
    {
        /// <summary>Идентификатор сотрудника</summary>
        [HiddenInput(DisplayValue = false)] // Признак того, что в представлении отображать это поле не стоит
        public int Id { get; set; }

        /// <summary>Фамилия</summary>
        [Display(Name = "Фамилия")]                                 // Что отображать в подписи к полю на интерфейсе
        [Required(ErrorMessage = "Фамилия является обязательной")]  // Поле обязательно! Указываем текст ошибки
        [MinLength(2)]                                              // Минмальная длина - 2 символа
        public string SurName { get; set; }

        /// <summary>Имя</summary>
        [Display(Name = "Имя"), Required(ErrorMessage = "Имя является обязательным")]
        [RegularExpression(@"(^[А-ЯЁ][а-яё]{2,150}$)|(^[A-Z][a-z]{2,150}$)", ErrorMessage = "Некорректный формат имени")] // Можно указать регулярное выражение
        public string FirstName { get; set; }

        /// <summary>Отчество</summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        /// <summary>Возраст</summary>
        [Display(Name = "Возраст")]
        [Range(18, 130)]                 // Можно ограничить диапазон значений
        public int Age { get; set; }
    }
}
