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
    /// <summary>Управление товарами</summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductController(IProductData ProductData) => _ProductData = ProductData;

        /// <summary>Получить секции товаров</summary>
        /// <returns>Существующие секции товаров</returns>
        [HttpGet("sections")]
        public IEnumerable<Section> GetSections()
        {
            return _ProductData.GetSections();
        }

        /// <summary>Получить бренды</summary>
        /// <returns>Существующие бренды</returns>
        [HttpGet("brands")]
        public IEnumerable<Brand> GetBrands()
        {
            return _ProductData.GetBrands();
        }

        /// <summary>Получить список товаров по указанным критериям фильтрации</summary>
        /// <param name="Filter">Критерии фильтрации товаров</param>
        /// <returns>Список отфильтрованных товаров</returns>
        [HttpPost, ActionName("Post")]
        public IEnumerable<ProductDTO> GetProducts([FromBody] ProductFilter Filter)
        {
            return _ProductData.GetProducts(Filter);
        }

        /// <summary>Получить товар по идентификатору</summary>
        /// <param name="id">Идентификатор товара</param>
        /// <returns>Товар с указанным идентификатором</returns>
        [HttpGet("{id}"), HttpGet("Get")]
        public ProductDTO GetProductById(int id)
        {
            return _ProductData.GetProductById(id);
        }
    }
}