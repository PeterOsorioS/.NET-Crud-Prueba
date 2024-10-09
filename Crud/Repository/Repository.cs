using Crud.Data;
using Crud.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Crud.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }
        public async Task AddAsync(T entity) 
        {
           await _context.AddAsync(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

        public int GetAllCount(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.Count();
        }

        public T GetById(int id)
        {
            return dbSet.FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query.FirstOrDefault();
        }

        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
