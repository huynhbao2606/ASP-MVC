using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP_MVC.ViewModels
{
    public class ProductDTO
    {
        public Product Product { get; set; }    
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
