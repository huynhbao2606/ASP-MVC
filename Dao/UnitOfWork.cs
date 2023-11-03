using ASP_MVC.Dao.IRepository;
using ASP_MVC.Data;
using ASP_MVC.Models;

namespace ASP_MVC.Dao
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Vaccine> _vaccineRepository;
        private IGenericRepository<Models.Type> _typeRepository;
        private IGenericRepository<VaccinationSchedule> _scheduleRepository;
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
        IGenericRepository<Vaccine> IUnitOfWork.VaccineRepository
        {
            get
            {
                if (_vaccineRepository == null)
                {
                    this._vaccineRepository = new GenericRepository<Vaccine>(_context);
                }

                return _vaccineRepository;
            }
        }
        IGenericRepository<Models.Type> IUnitOfWork.TypeRepository
        {
            get
            {
                if (_typeRepository == null)
                {
                    this._typeRepository = new GenericRepository<Models.Type>(_context);
                }

                return _typeRepository;
            }
        }
        IGenericRepository<VaccinationSchedule> IUnitOfWork.ScheduleRepository
        {
            get
            {
                if (_scheduleRepository == null)
                {
                    this._scheduleRepository = new GenericRepository<VaccinationSchedule>(_context);
                }

                return _scheduleRepository;
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