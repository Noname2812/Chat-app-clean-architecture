using ChatApp.Contract.Abstractions.Message;
using ChatApp.Domain.Enums;

namespace ChatApp.Contract.Services.V1.Message
{
    public static class Command
    {
        public record CreateMessageCommand(Guid CreateBy,Guid? RoomChatId, string? ToUser, Guid? MessageId, string Content, TypeMessage? Type, bool IsGroup, DateTimeOffset? CreateDate) 
            : ICommand;
    }
}
