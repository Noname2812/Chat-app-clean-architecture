using ChatApp.Contract.Abstractions.Message;
using ChatApp.Domain.Enums;

namespace ChatApp.Contract.Services.V1.Friend
{
    public static class Command
    {
        public record CreateRequestAddFriendCommand(Guid? From, Guid To, StatusFriend? Status = StatusFriend.Pending) : ICommand;
        public record UpdateStatusFriendCommand(Guid? From, Guid To, StatusFriend Status) : ICommand;
    }
}