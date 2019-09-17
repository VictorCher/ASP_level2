using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    /// <summary>Модель-представления бренда</summary>
    public class BrandViewModel : INamedEntity, IOrderedEntity
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Назнвание</summary>
        public string Name { get; set; }

        /// <summary>Порядковый номер</summary>
        public int Order { get; set; }

        /// <summary>Количество товара</summary>
        public int ProductsCount { get; set; }
    }
}
