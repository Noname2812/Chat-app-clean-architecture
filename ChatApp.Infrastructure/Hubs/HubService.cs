using ChatApp.Application.Abstractions.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Infrastructure.Hubs
{
    public class HubService : IHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public HubService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task JoinGroupChat(string ConnectionId, string RoomId)
        {
            await _hubContext.Groups.AddToGroupAsync(ConnectionId, RoomId);
        }

        public async Task NotifyTo(string From, string To, bool IsGroup, string Method, object value)
        {
            if (IsGroup == true)
            {
                await _hubContext.Clients.Group(To).SendAsync(Method, value);
            }
            else
            {
                await _hubContext.Clients.Client(To).SendAsync(Method, value);
            }
        }
    }
}
