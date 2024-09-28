using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.DTOs;
using ChatApp.Domain.Enums;

namespace ChatApp.Contract.Services.V1.Message
{
    public static class DomainEvent
    {
        public record UpdateStatusWhenMessageChangedEvent(Guid Id, Guid MessageId, StatusMessage StatusMessage) : IDomainEvent;
        public record SavedMessageEvent(Guid Id, string From,string? To, bool IsGroup, Domain.Entities.Message Message) : IDomainEvent;
    }
}
