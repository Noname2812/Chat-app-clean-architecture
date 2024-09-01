

using ChatApp.Contract.Abstractions.Message;

namespace ChatApp.Contract.Services.V1.RoomChat
{
    public static class Command
    {
        public record CreateRoomChatCommand(bool? IsGroup = false, string? Avatar = null,int? TopicBackgroundId = null, 
            string? Name = null,List<Member>? Members = null) : ICommand;
        public record Member(Guid Id, string Name, string? NickName);
    }

}
