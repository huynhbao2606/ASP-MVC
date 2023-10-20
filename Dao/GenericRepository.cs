using System;
using System.Linq.Expressions;
using ASP_MVC.Dao.IRepository;
using ASP_MVC.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC.Dao
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private DbSet<T> contextSet;

		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
			this.contextSet = _context.Set<T>(); //// create DbSet<T>
        }

		public void Add (T entity)
		{
			contextSet.Add(entity);
		}

		public void Update(T entity)
		{
			contextSet.Attach(entity);
			_context.Entry(entity).State = EntityState.Modified;
		}

		public T GetEntityById(int? id)
		{
			return contextSet.Find(id);
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
            IQueryable<T> query = contextSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                             .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return contextSet.ToList();
        }
        public void Delete(T entity)
        {
            if(_context.Entry(entity).State == EntityState.Detached)
            {
                contextSet.Attach(entity);
            }
            contextSet.Remove(entity);
        }
        public void DeleteById(object id)
        {
            T entityToDelete = contextSet.Find(id);
            Delete(entityToDelete);
        }
        public IEnumerable<T> GetEntities(Expression<Func<T, bool>> filter,
                                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            IQueryable<T> query = contextSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}