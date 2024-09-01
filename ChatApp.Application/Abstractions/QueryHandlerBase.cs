using AutoMapper;
using ChatApp.Domain.Abstractions.Repositories;

namespace ChatApp.Application.Abstractions
{
    public abstract class QueryHanlderBase<TEntity, TKey> where TEntity : class
    {
        protected readonly IRepositoryBase<TEntity, TKey> _repository;
        protected readonly IMapper _mapper;
        protected QueryHanlderBase(IRepositoryBase<TEntity, TKey> repositoryBase, IMapper mapper)
        {
            _repository = repositoryBase;
            _mapper = mapper;
        }
    }
}
