namespace ChatApp.Application.Abstractions.Services
{
    public interface IHubService
    {
        Task NotifyTo(string To, bool IsGroup , string Method, object value);
        Task JoinAllGroupChatsWithUserId(string ConnectionId, string UserId);
        Task AddMemberIntoGroup(string ConnectionId, string  GroupId);
        Task RemoveMemberInGroup(string ConnectionId, string GroupId);
        string GenarateTokenZegoClould(uint appID, string userID, string secret, long effectiveTimeInSeconds, string payload);
    }
}
