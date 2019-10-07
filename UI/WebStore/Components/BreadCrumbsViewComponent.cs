using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels.BreadCrumbs;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public BreadCrumbsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke(BreadCrumbType Type, int id, BreadCrumbType FromType)
        {
            switch (Type)
            {
                default: return View(new BreadCrumbViewModel[0]);

                case BreadCrumbType.Section:
                {
                    var section = _ProductData.GetSectionById(id);
                    return View(new[]
                    {
                       new BreadCrumbViewModel
                       {
                            BreadCrumbType = Type,
                            Id = id.ToString(),
                            Name = section.Name
                       }
                    });
                }

                case BreadCrumbType.Brand:
                {
                    var brand = _ProductData.GetBrandById(id);
                    return View(new[]
                    {
                        new BreadCrumbViewModel
                        {
                            BreadCrumbType = Type,
                            Id = id.ToString(),
                            Name = brand.Name
                        }
                    });
                }

                case BreadCrumbType.Product:
                    return View(GetProductBreadCrumbs(id, FromType, Type));
            }
        }
        private IEnumerable<BreadCrumbViewModel> GetProductBreadCrumbs(
            int id,
            BreadCrumbType FromType,
            BreadCrumbType Type)
        {
            var product = _ProductData.GetProductById(id);

            var crumbs = new List<BreadCrumbViewModel>();

            if (FromType == BreadCrumbType.Section)
                crumbs.Add(
                    new BreadCrumbViewModel
                    {
                        BreadCrumbType = FromType,
                        Id = product.Section.Id.ToString(),
                        Name = product.Section.Name
                    });
            else
                crumbs.Add(
                    new BreadCrumbViewModel
                    {
                        BreadCrumbType = FromType,
                        Id = product.Brand.Id.ToString(),
                        Name = product.Brand.Name
                    });

            crumbs.Add(new BreadCrumbViewModel
            {
                BreadCrumbType = Type,
                Id = product.Id.ToString(),
                Name = product.Name
            });

            return crumbs;
        }
    }
}
