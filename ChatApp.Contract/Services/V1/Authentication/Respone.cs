

using ChatApp.Contract.DTOs;

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
}
