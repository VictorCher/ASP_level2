using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("sections")]
        public IEnumerable<Section> GetSections()
        {
            return _ProductData.GetSections();
        }

        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands()
        {
            return _ProductData.GetBrands();
        }

        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter)
        {
            return _ProductData.GetProducts(Filter);
        }

        [HttpGet("{id}"), HttpGet("Get")]
        public ProductDTO GetProductById(int id)
        {
            return _ProductData.GetProductById(id);
        }
    }
}