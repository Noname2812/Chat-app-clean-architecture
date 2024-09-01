
using MediatR;

namespace ChatApp.Contract.Abstractions.Message
{
    public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
       where TEvent : IDomainEvent
    {
    }
}
