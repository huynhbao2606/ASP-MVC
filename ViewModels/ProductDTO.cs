using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ASP_MVC.ViewModels
{
    public class ProductDTO
    {
        public Product Product { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
