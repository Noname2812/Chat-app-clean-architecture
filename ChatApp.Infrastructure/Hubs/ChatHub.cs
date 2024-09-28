using ChatApp.Contract.DTOs;
using ChatApp.Contract.Services.V1.RoomChat;
using ChatApp.Domain.Abstractions;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Enums;
using ChatApp.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;
using static ChatApp.Contract.Services.V1.Message.Command;
using static ChatApp.Contract.Services.V1.Message.DomainEvent;
using static ChatApp.Contract.Services.V1.RoomChat.Command;


namespace ChatApp.Infrastructure.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub
    {
        private readonly IPublisher _publisher;
        private readonly ISender _sender;
        private readonly UserManager<AppUser> _userManager;

        public ChatHub(IPublisher publisher, ISender sender, UserManager<AppUser> userManager )
        {
            _publisher = publisher;
            _sender = sender;
            _userManager = userManager;
           
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            await _publisher.Publish(new SignedInHubEvent(Guid.NewGuid(), userId, Context.ConnectionId));
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            await _publisher.Publish(new SignedOutHubEvent(Guid.NewGuid(), userId));
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessageCommand msg)
        {
            try
            {

                var userId = Context.UserIdentifier;
                var roomId = msg.RoomChatId ?? Guid.NewGuid();
                if (msg.RoomChatId == null && msg.IsGroup == false && msg.ToUser != null)
                {
                    var from = await _userManager.FindByIdAsync(userId);
                    var members = new List<Member>() { new Member(Guid.Parse(userId),null), new Member(Guid.Parse(msg.ToUser), null) };
                    var createNewRoom = await _sender.Send(new CreateRoomChatCommand(roomId,false, null, null, null, members));
                    if (createNewRoom.IsFailure)
                    {
                        await Clients.Caller.SendAsync("ErrorWhileSendingMessage", createNewRoom.Error.Message);
                        return;
                    }
                }
                var messageId = Guid.NewGuid();
                var createDate = DateTimeOffset.Now;
                var result = await _sender.Send(new CreateMessageCommand(
                    Guid.Parse(userId), roomId, msg.ToUser, msg.MessageId, msg.Content, msg.Type, msg.IsGroup, createDate));
                if (result.IsSuccess)
                {
                    var message = new Domain.Entities.Message
                    {
                        Id = messageId,
                        Content = msg.Content,
                        Type = msg.Type ?? TypeMessage.String,
                        RoomChatId = roomId,
                        CreatedBy = Guid.Parse(userId),
                        CreatedDate = createDate,
                    };
                    await Task.WhenAll(_publisher.Publish(new SavedMessageEvent(Guid.NewGuid(), userId, roomId.ToString(), msg.IsGroup, message)),
                        Clients.Caller.SendAsync("SendMessageSuccessfully", message));
                }
                else
                {
                    await Clients.Caller.SendAsync("ErrorWhileSendingMessage", result.Error.Message);
                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("ErrorWhileSendingMessage", ex.Message);
            }
        }
    }
}
