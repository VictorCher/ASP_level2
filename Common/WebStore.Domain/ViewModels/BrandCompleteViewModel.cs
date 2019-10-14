using System.Collections.Generic;
using WebStore.ViewModels;

namespace WebStore.Domain.ViewModels
{
    public class BrandCompleteViewModel
    {
        public IEnumerable<BrandViewModel> Brands { get; set; }

        public int? CurrentBrandId { get; set; }
    }
}