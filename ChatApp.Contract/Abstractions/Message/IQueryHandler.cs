

using ChatApp.Contract.Abstractions.Shared;
using MediatR;

namespace ChatApp.Contract.Abstractions.Message
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
     where TQuery : IQuery<TResponse>
    {
    }
}
