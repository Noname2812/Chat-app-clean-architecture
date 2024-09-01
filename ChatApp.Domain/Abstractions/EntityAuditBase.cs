using ChatApp.Domain.Abstractions.Entities;

namespace ChatApp.Domain.Abstractions
{
    public abstract class EntityAuditBase<TKey> : EntityBase<TKey>, IAuditable
    {
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
