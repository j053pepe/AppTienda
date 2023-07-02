using System.Linq.Expressions;

namespace Core.Contracts.Data
{
    public partial interface IRepository<T> : IDisposable where T : class
    {

        T GetById(object id);

        Task<T> GetByIdAsync(object id);

        Task Insert(T entity);

        Task Insert(IEnumerable<T> entities);

        Task Update(T entity);

        Task Update(IEnumerable<T> entities);

        Task Delete(T entity);

        Task Delete(IEnumerable<T> entities);

        Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, Task<IOrderedQueryable<T>>> orderBy = null,
            string includeProperties = "");

        IEnumerable<T> GetPagedElements<S>(int pageIndex, int pageCount,
            Expression<Func<T, S>> orderByExpression, bool ascending,
            Expression<Func<T, bool>> filter = null, string includeProperties = "");

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }

        Task CancelChanges(T entity);
    }
}
