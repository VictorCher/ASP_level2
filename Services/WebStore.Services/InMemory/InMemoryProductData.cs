using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.DTO;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Services.Map;

namespace WebStore.Infrastructure.Implementations
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public Section GetSectionById(int id) => TestData.Sections.FirstOrDefault(s => s.Id == id);

        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Brand GetBrandById(int id) => TestData.Brands.FirstOrDefault(b => b.Id == id);

        public PagedProductDTO GetProducts(ProductFilter Filter)
        {
            var products = TestData.Products;

            if (Filter?.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);
            if (Filter?.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);

            var total_count = products.Count();

            if (Filter?.PageSize != null)
                products = products
                   .Skip((Filter.Page - 1) * (int)Filter.PageSize)
                   .Take((int)Filter.PageSize);

            return new PagedProductDTO
            {
                Products = products
                   .AsEnumerable()
                   .Select(ProductMapper.ToDTO),
                TotalCount = total_count
            };
        }

        public ProductDTO GetProductById(int id) => 
            TestData.Products.FirstOrDefault(p => p.Id == id).ToDTO();
    }
}
