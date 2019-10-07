using System.Collections.Generic;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Interfaces
{
    /// <summary>Сервис товаров</summary>
    public interface IProductData
    {
        IEnumerable<Section> GetSections();

        IEnumerable<Brand> GetBrands();

        IEnumerable<ProductDTO> GetProducts(ProductFilter Filter);

        ProductDTO GetProductById(int id);
    }
}
