

using ChatApp.Contract.Abstractions.Message;
using static ChatApp.Contract.Services.V1.Identty.Respone;


namespace ChatApp.Contract.Services.V1.Identty
{
    public static class Query
    {
        public record LoginQuery(string Email, string Password) : IQuery<LoginSuccessRespone>;
        public record RefreshTokenQuery(string? AccessToken, string? RefreshToken) : IQuery<AuthenticatedRespone>;
    }
}
