
using ChatApp.Contract.Abstractions.Shared;
using MediatR;

namespace ChatApp.Contract.Abstractions.Message
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
