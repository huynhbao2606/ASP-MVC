using ASP_MVC.Dao.IRepository;
using ASP_MVC.Models;
using ASP_MVC.Helpers;

namespace ASP_MVC.Services
{
    public class BasketService : IBasketService
    {
        const string ShoppingCartSessionVarible = "_ShoppingCartSessionVarible";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public BasketService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
        }

        public void AddItem(int id, int quanlity)
        {
            List<BasketItem> shoppingCartList;
            if (_contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVarible) != default) {

                shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVarible);

                if (shoppingCartList.Where(i => i.Product.Id == id).Any())
                {
                    shoppingCartList.Where(i => i.Product.Id == id).Select(x =>
                    {
                        x.Count += quanlity;
                        return x;
                    }).ToList();

                }else
                {
                    shoppingCartList.Add(new BasketItem
                    {

                        Count = quanlity,
                        Product = _unitOfWork.ProductRepository.GetEntityById(id)

                    });
                }

            } else
            {
                shoppingCartList = new List<BasketItem>
                {
                    new BasketItem
                    {
                        Count = quanlity,
                        Product = _unitOfWork.ProductRepository.GetEntityById(id)
                    }
                };
            }

            _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(ShoppingCartSessionVarible, shoppingCartList);

         }


        public void RemoveItem(int id)
        {
            if (_contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVarible) != default)
            {
                List<BasketItem> shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(ShoppingCartSessionVarible);

                shoppingCartList.RemoveAll(i => i.Product.Id == id);

                _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(ShoppingCartSessionVarible, shoppingCartList);
            }


        }
        
        public void ClearBasket()
        {
            _contextAccessor.HttpContext.Session.Remove(ShoppingCartSessionVarible);
        }
    }
}
