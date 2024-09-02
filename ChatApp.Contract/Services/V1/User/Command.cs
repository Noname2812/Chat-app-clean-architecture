using ChatApp.Contract.Abstractions.Message;

namespace ChatApp.Contract.Services.V1.User
{
    public static class Command
    {
        public record UpadteUserCommand(Guid? UserId, string? CurrentPassword, string? NewPassword, string? Name, string? Avatar, DateTimeOffset? DayOfBirth) : ICommand;
    }
}
