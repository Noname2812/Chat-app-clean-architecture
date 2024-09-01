

using ChatApp.Domain.Abstractions.Entities;

namespace ChatApp.Domain.Abstractions
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
