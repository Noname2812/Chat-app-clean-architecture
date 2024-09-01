using ChatApp.Domain.Abstractions;
using ChatApp.Domain.Abstractions.Entities;
using ChatApp.Domain.Entities.Identity;

namespace ChatApp.Domain.Entities
{
    public class ConversationParticipant :  EntityBase<Guid>, IDateTracking
    {
        public Guid UserId { get; set; }
        public Guid RoomChatId { get; set; }
        public string? RoomName { get; set; }
        public string  NickName { get; set; }
        public DateTimeOffset CreatedDate { get ; set ; }
        public DateTimeOffset ModifiedDate { get ; set ; }
        public virtual RoomChat RoomChat { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
