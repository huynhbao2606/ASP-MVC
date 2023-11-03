using ASP_MVC.Helpers;
using ASP_MVC.Services;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using ASP_MVC.ViewModels;

namespace ASP_MVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        const string ShoppingCartSessionVariable = "_ShoppingCartSessionVariable";
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        // GET: BasketController
        public IActionResult Index()
        {
            List<BasketItem> shoppingCartList;
            if (HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default)
            {
                shoppingCartList = HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);
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


        [HttpGet]
        public IActionResult GetAll()
        {
            List<BasketItem> shoppingCartList;
            if (HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default)
            {
                shoppingCartList = HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);
            }
            else
            {
                shoppingCartList = new List<BasketItem>();
            }

            return Json(new { data = shoppingCartList });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart([FromBody] List<UpdateCartDTO> data)
        {
            if (HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable) != default)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Count != 0)
                    {
                        int id = data[i].Id;
                        int newQuantity = data[i].Count;

                        List<BasketItem> itemList = HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVariable);

                        if (itemList.Where(i => i.Product.Id == id).Any())
                        {
                            int currentQuantity = itemList.Where(i => i.Product.Id == id).Select(i => i.Count).FirstOrDefault();
                            int change = newQuantity - currentQuantity;
                            _basketService.AddItem(data[i].Id, change);
                        }
                    }
                    else
                    {
                        _basketService.RemoveItem(data[i].Id);
                    }
                }
            }
            return Json(new { redirectToUrl = Url.Action("Index", "Basket") });
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
