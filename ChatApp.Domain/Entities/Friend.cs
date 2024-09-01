

using ChatApp.Domain.Abstractions.Entities;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Enums;

namespace ChatApp.Domain.Entities
{
    public class Friend : IDateTracking
    {
        public Guid UserId { get; set; }
        public Guid FriendId { get; set; }
        public StatusFriend Status { get; set; }
        public DateTimeOffset CreatedDate { get ; set ; }
        public DateTimeOffset ModifiedDate { get ; set ; }
        public virtual AppUser UserFriend { get; set; }
        public virtual AppUser UserOf {  get; set; }
    }
}
