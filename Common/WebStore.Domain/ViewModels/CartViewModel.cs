using System.Collections.Generic;
using System.Linq;

namespace WebStore.ViewModels
{
    /// <summary>Модель-представления корзины</summary>
    public class CartViewModel
    {
        /// <summary>Элементы корзины</summary>
        public Dictionary<ProductViewModel, int> Items { get; set; } = new Dictionary<ProductViewModel, int>();

        /// <summary>Полное количество товара</summary>
        public int ItemsCount => Items?.Sum(item => item.Value) ?? 0;
    }
}
