

using ChatApp.Contract.Abstractions.Shared;
using MediatR;

namespace ChatApp.Contract.Abstractions.Message;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}