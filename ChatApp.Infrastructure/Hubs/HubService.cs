﻿using ChatApp.Application.Abstractions.Services;
using ChatApp.Infrastructure.Dapper.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Infrastructure.Hubs
{
    public class HubService : IHubService
    {
        private readonly RoomChatQueryRepository _roomChatQueryRepository;
        private readonly IHubContext<ChatHub> _hubContext;
        public HubService(IHubContext<ChatHub> hubContext, RoomChatQueryRepository roomChatQuery)
        {
            _hubContext = hubContext;
            _roomChatQueryRepository = roomChatQuery;
        }

        public async Task AddMemberIntoGroup(string ConnectionId, string GroupId)
        {
            await _hubContext.Groups.AddToGroupAsync(ConnectionId, GroupId);
        }

        public string GenarateTokenZegoClould(uint appID, string userID, string secret, long effectiveTimeInSeconds, string payload)
        {
            var token = GenarateTokenZegoCloud.GenerateToken04(appID, userID, secret, effectiveTimeInSeconds, payload);
            return token.token;
        }

        public async Task JoinAllGroupChatsWithUserId(string ConnectionId, string UserId)
        {
            var rooms = await _roomChatQueryRepository.GetIdRoomChatsByUserId(Guid.Parse(UserId));
            foreach (var room in rooms)
            {
                if (room.IsGroup)
                {
                    await _hubContext.Groups.AddToGroupAsync(ConnectionId, room.Id.ToString());
                }
            }
        }

        public async Task NotifyTo(string To, bool IsGroup, string Method, object value)
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

        public async Task RemoveMemberInGroup(string ConnectionId, string GroupId)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(ConnectionId, GroupId);
        }
    }
}
