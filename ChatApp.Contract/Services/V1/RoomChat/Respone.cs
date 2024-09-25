using ChatApp.Contract.DTOs;

namespace ChatApp.Contract.Services.V1.RoomChat
{
    public static class Respone
    {
        public class RoomChatRespone
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Avatar {  get; set; }
            public bool IsGroup { get; set; } = false;
            public List<ConversationParticipantRespone>? ConversationParticipants { get; set; }
            public List<MessageDTO>? Messages { get; set; }
        }
        public class ConversationParticipantRespone
        {
            public InfoMember AppUser { get; set; }
            public string NickName { get; set; }
        }
        public class InfoMember
        {
            public Guid Id { get; set; }
            public string? Avatar { get; set; }
            public string? Name { get; set; }
            public bool? IsOnline { get; set; }
            public DateTimeOffset? LastOnline { get; set; }
        }
    }
}
