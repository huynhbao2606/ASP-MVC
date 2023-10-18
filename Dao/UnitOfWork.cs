using ASP_MVC.Dao;
using ASP_MVC.Data;
using ASP_MVC.Models;

namespace ASP_MVC.Dao
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<CoverType> _coverTypeRepository;
        private GenericRepository<Product> _productRepository;

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

        GenericRepository<CoverType> IUnitOfWork.CoverTypeRepository
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


        GenericRepository<Product> IUnitOfWork.ProductRepository
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