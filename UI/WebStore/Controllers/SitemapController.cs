using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IProductData _ProductData;

        public SitemapController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index()
        {
            var nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", "Home")),
                new SitemapNode(Url.Action("Shop", "Catalog")),
                new SitemapNode(Url.Action("BlogSingle", "Home")),
                new SitemapNode(Url.Action("Blog", "Home")),
                new SitemapNode(Url.Action("ContactUs", "Home")),
            };

            foreach (var section in _ProductData.GetSections())
                if(section.ParentId != null)
                    nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new { SectionId = section.Id })));

            foreach (var brand in _ProductData.GetBrands())
                nodes.Add(new SitemapNode(Url.Action("Shop", "Catalog", new { BrandId = brand.Id })));

            foreach (var product in _ProductData.GetProducts(new ProductFilter()))
                nodes.Add(new SitemapNode(Url.Action("ProductDetails", "Catalog", new { product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
