using System.Linq.Expressions;

namespace Crud.Repository.IRepository
{
    public interface IRepository<T> where T: class
    {
        T GetById(int id);
        void Add(T entity);
        Task AddAsync(T entity);
        IEnumerable<T> GetAll(
           Expression<Func<T, bool>>? filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           string? includeProperties = null
       );
        int GetAllCount(Expression<Func<T, bool>>? filter = null);
        T GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null
        );
        Task Remove(T entity);
        void Save();
        Task SaveAsync();
    }
}
