using ChatApp.Domain.Abstractions.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security;

namespace ChatApp.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<Guid>, IAuditable
    {
        public string Description { get; set; }
        public string RoleCode { get; set; }

        public virtual ICollection<IdentityUserRole<Guid>> UserRoles { get; set; }
        public virtual ICollection<IdentityRoleClaim<Guid>> Claims { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public bool IsDeleted { get ; set ; }
        public DateTimeOffset? DeletedDate { get ; set ; }
        public Guid CreatedBy { get ; set ; }
        public Guid ModifiedBy { get ; set ; }
        public DateTimeOffset CreatedDate { get ; set ; }
        public DateTimeOffset ModifiedDate { get ; set ; }
    }
}
