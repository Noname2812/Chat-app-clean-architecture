



using ChatApp.Domain.Abstractions;

namespace ChatApp.Domain.Entities
{
    public class RoomChat : EntityAuditBase<Guid>
    {
        public string?  Avatar { get; set; }
        public bool IsGroup { get; set; } = false;
        public string? Name { get; set; }
        //public int? TopicBackgroundId { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; }
        //public virtual TopicBackgroundRoomChat TopicBackgroundRoomChat { get; set; }
    }
}
