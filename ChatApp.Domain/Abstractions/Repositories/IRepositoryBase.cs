

using System.Linq.Expressions;

namespace ChatApp.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity, in Tkey> where TEntity : class // => In implementation should be EntityBase<TKey>
    {
        Task<TEntity?> FindByIdAsync(Tkey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<int> GetTotalCount(Expression<Func<TEntity, bool>>? predicate = null);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        void RemoveMultiple(List<TEntity> entities);
    }
}
