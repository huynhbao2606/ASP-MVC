using System;
using System.Linq.Expressions;
using ASP_MVC.Dao.IRepository;
using ASP_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC.Dao
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
        private readonly ApplicationDbContext _db;
        private DbSet<T> dbSet;

        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>(); //// create DbSet<T>
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
        }

        public T GetEntityById(int? id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Delete(T entity)
        {
            if (_db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void DeleteById(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }


        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
                                            string includeProperties)
        {
            IQueryable<T> query = dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null && includeProperties != "")
            {
                string[] splitedIncludeProperties =
                    includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var property in splitedIncludeProperties)
                {
                    query = query.Include(property);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}