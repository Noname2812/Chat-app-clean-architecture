

using ChatApp.Domain.Abstractions.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChatApp.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>, IDateTracking
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
        public bool IsOnline { get; set; }
        public bool IsVertify { get; set; }
        public DateTimeOffset? DayOfBirth { get; set; }
        public DateTimeOffset LastOnline { get; set ; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
        public virtual ICollection<ConversationParticipant> Conversations { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Friend> FriendOf { get; set; }
        public DateTimeOffset CreatedDate { get ; set ; }
        public DateTimeOffset ModifiedDate { get ; set ; }

    }
}
