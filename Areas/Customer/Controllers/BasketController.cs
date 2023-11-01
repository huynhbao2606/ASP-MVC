using ASP_MVC.Helpers;
using ASP_MVC.Models;
using ASP_MVC.Services;

using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        const string ShoppingCartSessionVarible = "_ShoppingCartSessionVarible";
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        // GET: BasketController
        public ActionResult Index()
        {
            List<BasketItem> shoppingCartList;
            if(HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVarible) != default)
            {
                shoppingCartList = HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVarible);
            }
            else
            {
                shoppingCartList = new List<BasketItem>();
            }
            return View(shoppingCartList);
        }

        public IActionResult AddToCart(int id)
        {
            _basketService.AddItem(id, 1);
            return RedirectToAction("Index", "Home");
        }
        // GET: BasketController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BasketController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BasketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BasketController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BasketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BasketController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BasketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
