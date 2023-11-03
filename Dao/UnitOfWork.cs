using ASP_MVC.Dao.IRepository;
using ASP_MVC.Data;
using ASP_MVC.Models;

namespace ASP_MVC.Dao
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Category> _categoryRepository;
        private IGenericRepository<CoverType> _coverTypeRepository;
        private IGenericRepository<Product> _productRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }


        IGenericRepository<Category> IUnitOfWork.CategoryRepository
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

        IGenericRepository<CoverType> IUnitOfWork.CoverTypeRepository
        {
            get
            {
                if (_coverTypeRepository == null)
                {
                    this._coverTypeRepository = new GenericRepository<CoverType>(_context);
                }

                return _coverTypeRepository;
            }
        }


        IGenericRepository<Product> IUnitOfWork.ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(_context);
                }

                return _productRepository;
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