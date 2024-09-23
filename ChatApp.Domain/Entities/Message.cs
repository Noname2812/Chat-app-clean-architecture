

using ChatApp.Domain.Abstractions;
using ChatApp.Domain.Enums;

namespace ChatApp.Domain.Entities
{
    public class Message : EntityAuditBase<Guid>
    {
        public string? Content { get; set; }
        public TypeMessage Type { get; set; }
        public StatusMessage Status { get; set; }
        public Guid RoomChatId { get; set; }
        public virtual RoomChat RoomChat { get; set; }
        public string SeenByJson { get; set; }
    }
}
