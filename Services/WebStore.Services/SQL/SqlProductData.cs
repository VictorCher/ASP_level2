using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.DTO.Product;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Implementations
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _db;

        public SqlProductData(WebStoreContext DB) => _db = DB;

        public IEnumerable<Section> GetSections() => _db.Sections
            //.Include(s => s.Products)
            .AsEnumerable();

        public IEnumerable<Brand> GetBrands() => _db.Brands
            //.Include(brand => brand.Products)
            .AsEnumerable();

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            IQueryable<Product> products = _db.Products;
            if (Filter is null)
                return products
                       .AsEnumerable()
                       .Select(p => new ProductDTO
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Order = p.Order,
                            ImageUrl = p.ImageUrl,
                            Price = p.Price,
                            Brand = p.Brand is null ? null : new BrandDTO
                            {
                                Id = p.Brand.Id,
                                Name = p.Brand.Name
                            }
                        });

            if (Filter.SectionId != null)
                products = products.Where(product => product.SectionId == Filter.SectionId);

            if (Filter.BrandId != null)
                products = products.Where(product => product.BrandId == Filter.BrandId);

            return products
               .AsEnumerable()
               .Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Order = p.Order,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Brand = p.Brand is null ? null : new BrandDTO
                    {
                        Id = p.Brand.Id,
                        Name = p.Brand.Name
                    }
                });
        }

        public ProductDTO GetProductById(int id)
        {
            var p = _db.Products
               .Include(product => product.Brand)
               .Include(product => product.Section)
               .FirstOrDefault(product => product.Id == id);
            return new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Order = p.Order,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Brand = p.Brand is null ? null : new BrandDTO
                {
                    Id = p.Brand.Id,
                    Name = p.Brand.Name
                }
            };
        }
    }
}
