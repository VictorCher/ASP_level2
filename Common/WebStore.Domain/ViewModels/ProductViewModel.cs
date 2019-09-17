using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    /// <summary>Модель-представления товара</summary>
    public class ProductViewModel : INamedEntity, IOrderedEntity
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Название</summary>
        public string Name { get; set; }

        /// <summary>Порядковый номер</summary>
        public int Order { get; set; }

        /// <summary>Ссылка на изображение</summary>
        public string ImageUrl { get; set; }

        /// <summary>Цена</summary>
        public decimal Price { get; set; }

        /// <summary>Бренд</summary>
        public string Brand { get; set; }
    }
}
