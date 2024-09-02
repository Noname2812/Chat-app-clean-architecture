

namespace ChatApp.Contract.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public bool? IsVertify { get; set; }
        public string? Avatar { get; set; }
        public string? Name { get; set; }
        public DateTimeOffset? DayOfBirth { get; set; }
    }
}
