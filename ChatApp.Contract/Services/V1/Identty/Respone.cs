

namespace ChatApp.Contract.Services.V1.Identty;

public static class Respone
{
    public class AuthenticatedRespone
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpriedtime { get; set; }
    }
    public class LoginSuccessRespone
    {
        public UserDTO User { get; set; }
        public AuthenticatedRespone Token { get; set; }
    }
    public class UserDTO
    {
        public Guid Id { get; set; }    
        public string Email { get; set; }
        public bool IsVertify { get; set; }
        public string Avatar { get; set; }
        public DateTimeOffset? DayOfBirth { get; set; }
    }
}
