using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    /// <summary>Сервис корзины</summary>
    public interface ICartService
    {
        /// <summary>Добавить товар в корзину</summary>
        /// <param name="id">Идентификатор добавляемого товара</param>
        void AddToCart(int id);

        /// <summary>Уменьшить количество товара в корзине на единицу</summary>
        /// <param name="id">Идентификатор товара</param>
        void DecrementFromCart(int id);

        /// <summary>Удалить товар из корзины</summary>
        /// <param name="id">Идентификатор товара</param>
        void RemoveFromCart(int id);

        /// <summary>Очистить корзину</summary>
        void RemoveAll();

        /// <summary>Создать модель-представления корзины</summary>
        /// <returns>Модель-представления корзины</returns>
        CartViewModel TransformCart();
    }
}
