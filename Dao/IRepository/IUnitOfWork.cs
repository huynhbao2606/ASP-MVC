using ASP_MVC.Models;

namespace ASP_MVC.Dao.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<CoverType> CoverTypeRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
    }
}