
namespace ChatApp.Domain.Abstractions.Entities
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
        public void Undo()
        {
            IsDeleted = false;
            DeletedDate = null;
        }
    }
}
