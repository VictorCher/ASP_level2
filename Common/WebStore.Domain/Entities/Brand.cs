using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities
{
    /// <summary>Бренд</summary>
    [Table("Brands")] // Насильно указываем желаемое имя таблицы. Если этого не сделать, то таблица будет Brand
    public class Brand : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        // virtual - указание Entity Framework на то, что Products должно быть навигационным свойством!
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
