using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.ViewModels
{
    public class PageViewModel
    {
        public int TotalItmes { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages => (int) Math.Ceiling((decimal) TotalItmes / PageSize);
    }
}
