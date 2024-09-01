

using MediatR;

namespace ChatApp.Contract.Abstractions.Message
{
    public interface IDomainEvent : INotification
    {
        public Guid IdEvent { get; init; }
    }
}
