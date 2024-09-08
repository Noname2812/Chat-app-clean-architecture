namespace ChatApp.Application.Abstractions.Services
{
    public interface IHubService
    {
        Task NotifyTo(string From,string To, bool IsGroup , string Method, object value);
        Task JoinGroupChat(string ConnectionId, string UserId);
    }
}
