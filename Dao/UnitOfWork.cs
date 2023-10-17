using ASP_MVC.Dao;
using ASP_MVC.Data;
using ASP_MVC.Models;

namespace ASP_MVC.Dao
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Category> _categoryRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }


        GenericRepository<Category> IUnitOfWork.CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    this._categoryRepository = new GenericRepository<Category>(_context);
                }

                return _categoryRepository;
            }
        }

        void IDisposable.Dispose()
        {
            _context.Dispose();
        }

        void IUnitOfWork.Save()
        {
            _context.SaveChanges();
        }
    }
}