

using ChatApp.Contract.Abstractions.Message;
using ChatApp.Domain.Entities.Identity;

namespace ChatApp.Contract.Services.V1.Identty
{
    public static class DomainEvent
    {
        public record SignedOutEvent(Guid Id, Guid UserId, string Token) : IDomainEvent;
        public record SignedInEvent(Guid Id, AppUser User, string RefreshToken) : IDomainEvent;
    }
}
