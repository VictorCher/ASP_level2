using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities.Base
{
    /// <summary>Сущность</summary>
    public abstract class BaseEntity : IBaseEntity
    {
        [Key] // Указание на то, что свойство является первичным ключём таблицы
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Требование для БД устанавливать значение данного свойства при добавлении записи в таблицу
        public int Id { get; set; }
    }
}
