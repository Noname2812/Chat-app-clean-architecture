

using ChatApp.Domain.Abstractions;

namespace ChatApp.Domain.Entities
{
    public class TopicBackgroundRoomChat : EntityAuditBase<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<RoomChat> RoomChats { get; set;}
    }
}
