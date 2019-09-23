using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.DTO
{
    public class BrandDTO : INamedEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
