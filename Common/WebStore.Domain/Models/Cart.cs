using System.Collections.Generic;
using System.Linq;

namespace WebStore.Models
{
    /// <summary>Корзина</summary>
    public class Cart
    {
        /// <summary>Записи корзины</summary>
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        /// <summary>Общее количество товара</summary>
        public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;
    }
}