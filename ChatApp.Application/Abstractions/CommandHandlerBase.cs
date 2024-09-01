

using ChatApp.Domain.Abstractions;
using ChatApp.Domain.Abstractions.Repositories;

namespace ChatApp.Application.Abstractions
{
    public abstract class CommandHandlerBase<TEntity, TKey> where TEntity : class
    {
        protected readonly IRepositoryBase<TEntity, TKey> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected CommandHandlerBase(IRepositoryBase<TEntity, TKey> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
    }
}
