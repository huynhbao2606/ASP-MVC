using ASP_MVC.Dao;
using ASP_MVC.Dao.IRepository;

namespace ASP_MVC.Services
{
    public interface IBasketService
    {

        void AddItem(int item, int quanlity);

        void RemoveItem(int item);
    }
}
