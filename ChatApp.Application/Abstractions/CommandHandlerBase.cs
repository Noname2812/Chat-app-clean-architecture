using ChatApp.Domain.Abstractions.Repositories;

namespace ChatApp.Application.Abstractions
{
    public abstract class CommandHandlerBase<TEntity, TKey> where TEntity : class
    {
        protected readonly IRepositoryBase<TEntity, TKey> _repository;
        protected CommandHandlerBase(IRepositoryBase<TEntity, TKey> repository)
        {
            _repository = repository;
        }
    }
}
