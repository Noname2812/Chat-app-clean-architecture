namespace ChatApp.Application.Abstractions.Services
{
    public interface IHubService
    {
        Task NotifyTo(string From,string To, bool IsGroup , string Method, object value);
        Task JoinAllGroupChatsWithUserId(string ConnectionId, string UserId);
        Task AddMemberIntoGroup(string ConnectionId, string  GroupId);
    }
}
