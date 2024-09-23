using ChatApp.Domain.Enums;
using static ChatApp.Contract.Services.V1.Message.Respone;
namespace ChatApp.Contract.DTOs
{
    public class MessageDTO
    {
        public Guid? Id { get; set; }
        public Guid RoomChatId { get; set; }
        public string Content { get; set; }
        public TypeMessage Type { get; set; }
        public Guid CreatedBy { get; set; }
        public List<SeenBy>? SeenBys { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}
