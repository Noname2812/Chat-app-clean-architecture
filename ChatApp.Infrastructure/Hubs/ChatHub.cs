using ChatApp.Contract.DTOs;
using ChatApp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;
using static ChatApp.Contract.Services.V1.Message.Command;
using static ChatApp.Contract.Services.V1.Message.DomainEvent;


namespace ChatApp.Infrastructure.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IPublisher _publisher;
        private readonly ISender _sender;
        public ChatHub(IPublisher publisher, ISender sender)
        {
            _publisher = publisher;
            _sender = sender;

        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _publisher.Publish(new SignedInHubEvent(Guid.NewGuid(), userId, Context.ConnectionId));
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _publisher.Publish(new SignedOutHubEvent(Guid.NewGuid(), userId));
        }

        public async Task SendMessage(CreateMessageCommand msg)
        {
            var messageId = Guid.NewGuid();
            var createDate = DateTimeOffset.Now;
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _sender.Send(new CreateMessageCommand(Guid.Parse(userId),msg.RoomChatId, msg.MessageId, msg.Content, msg.Type, msg.IsGroup, createDate));
            if (result.IsSuccess)
            {
                var message = new MessageDTO
                {
                    Id = messageId,
                    Content = msg.Content,
                    Type = (TypeMessage)msg.Type,
                    RoomChatId = msg.RoomChatId,
                    CreatedBy = Guid.Parse(userId),
                    CreatedDate = createDate,
                };
                await Task.WhenAll(_publisher.Publish(new SavedMessageEvent(Guid.NewGuid(), Context.ConnectionId, msg.RoomChatId.ToString(), msg.IsGroup, message)),
                    Clients.Caller.SendAsync("SendMessageSuccessfully", message));
            }
            else
            {
                await Clients.Caller.SendAsync("ErrorWhileSendingMessage", result.Error);
            }
        }
    }
}
