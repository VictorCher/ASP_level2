using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    /// <summary>Модель-представления заказа</summary>
    public class OrderViewModel
    {
        /// <summary>Название</summary>
        [Required]
        public string Name { get; set; }

        /// <summary>Телефон</summary>
        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        /// <summary>Адрес</summary>
        [Required]
        public string Address { get; set; }
    }
}
