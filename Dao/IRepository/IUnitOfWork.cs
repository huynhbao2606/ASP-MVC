using ASP_MVC.Models;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace ASP_MVC.Dao.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<CoverType> CoverTypeRepository { get; }
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Vaccine> VaccineRepository { get; }
        IGenericRepository<Models.Type> TypeRepository { get; }
        IGenericRepository<VaccinationSchedule> ScheduleRepository { get; }

    }
}