using ASP_MVC.Dao.IRepository;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace ASP_MVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller 
    // GET: Admin/Product
    
    {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork,ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            if (page == null) page = 1;

            int pageSize = 4;


            int pageNumber = (page ?? 1);

            IEnumerable<Product> productList = _unitOfWork.ProductRepository.GetEntities(
                filter: null,
                orderBy: null,
                includeProperties: "Category,CoverType,Vaccine"
            );


            return View(productList.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}