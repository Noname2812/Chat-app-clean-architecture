

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChatApp.Domain.Abstractions.Repositories
{
    internal interface IRepositoryDbContextBase<TContext, TEntity,in Tkey> where TContext : DbContext
        where TEntity : class // => In implementation should be EntityBase<TKey>
    {
        Task<TEntity> FindByIdAsync(Tkey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includes);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveMutiple(List<TEntity> entities);
    }
}
