

using ChatApp.Contract.Abstractions.Shared;
using MediatR;

namespace ChatApp.Contract.Abstractions.Message
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
     where TCommand : ICommand
    { }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    { }
}
