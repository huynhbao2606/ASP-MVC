using ASP_MVC.Dao;
using ASP_MVC.Models;

namespace ASP_MVC.Dao
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<CoverType> CoverTypeRepository { get; }
        GenericRepository<Product> ProductRepository { get; }
    }
}