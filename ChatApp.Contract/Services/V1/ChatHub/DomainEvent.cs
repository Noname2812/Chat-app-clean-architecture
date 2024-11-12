using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Services.V1.ChatHub.Common;
using ChatApp.Domain.Enums;

namespace ChatApp.Contract.Services.V1.ChatHub
{
    public static class DomainEvent
    {
        public record SignedInHubEvent(Guid Id, string UserId, string ConnectionId) : IDomainEvent;
        public record SignedOutHubEvent(Guid Id, string UserId) : IDomainEvent;
        public record RecievedRequestCallPrivateEvent(Guid Id, Guid Caller, Domain.Entities.ConversationParticipant? To, Domain.Entities.RoomChat RoomChat, TypeCall Type) : IDomainEvent;
        public record AcceptedRequestCallEvent(Guid Id, ParamsCreateTokenZegoCloud Params, Guid Self, Domain.Entities.RoomChat RoomChat) : IDomainEvent;
        public record StopedCallPrivateEvent(Guid Id, Guid Caller) : IDomainEvent;
    }
}
