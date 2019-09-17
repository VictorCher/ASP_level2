using System.Collections.Generic;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.ViewModels
{
    /// <summary>Модель-представления секции</summary>
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }

        /// <summary>Название</summary>
        public string Name { get; set; }

        /// <summary>Порадковый номер</summary>
        public int Order { get; set; }

        /// <summary>Дочерние секции</summary>
        public List<SectionViewModel> ChildSections { get; set; }  = new List<SectionViewModel>();

        /// <summary>Родительская секция</summary>
        public SectionViewModel ParentSection { get; set; }
    }
}
