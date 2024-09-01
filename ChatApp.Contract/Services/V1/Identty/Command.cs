using ChatApp.Contract.Abstractions.Message;

namespace ChatApp.Contract.Services.V1.Identty
{
    public static class Command
    {
        public record RegisterAccountCommand(string? Email, string? Password, string? Name) : ICommand;
        public record UpdateProfileCommand(Guid Id, string? Name) : ICommand;
        public record LogoutCommand(string Token) : ICommand;
    }
}
