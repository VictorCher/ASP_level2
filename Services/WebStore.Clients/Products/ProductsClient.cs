using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, "api/product") { }

        public IEnumerable<Section> GetSections() =>
            Get<List<Section>>($"{_ServiceAddress}/sections");

        public IEnumerable<Brand> GetBrands() =>
            Get<List<Brand>>($"{_ServiceAddress}/brands");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter)
        {
            return Post(_ServiceAddress, Filter)
               .Content
               .ReadAsAsync<List<ProductDTO>>()
               .Result;
        }

        public ProductDTO GetProductById(int id)
        {
            return Get<ProductDTO>($"{_ServiceAddress}/{id}");
        }
    }
}
