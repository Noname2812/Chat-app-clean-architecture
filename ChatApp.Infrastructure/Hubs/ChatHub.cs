using ChatApp.Contract.Services.V1.RoomChat;
using ChatApp.Domain.Entities.Identity;
using ChatApp.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using static ChatApp.Contract.Services.V1.ChatHub.DomainEvent;
using static ChatApp.Contract.Services.V1.ChatHub.Request;
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

        public ChatHub(IPublisher publisher, ISender sender, UserManager<AppUser> userManager)
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
                    var members = new List<Member>() { new Member(Guid.Parse(userId), null), new Member(Guid.Parse(msg.ToUser), null) };
                    var createNewRoom = await _sender.Send(new CreateRoomChatCommand(roomId, false, null, null, null, members));
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
        public async Task CreateRequestCall(CallRequest request)
        {
            var userId = Context.UserIdentifier;
            if (request.RoomChat.IsGroup)
            {
                // publish event group call
            }
            else
            {
                var to = request.RoomChat.ConversationParticipants.Where(x => x.UserId != Guid.Parse(userId)).FirstOrDefault();
                await _publisher.Publish(new RecievedRequestCallPrivateEvent(Guid.NewGuid(), Guid.Parse(userId), to, request.RoomChat, request.Type));
            }
        }
        public async Task CancelCallReqest()
        {

        }
        public async Task AcceptRequestCall(AcceptCallRequest request)
        {

           
            var userId = Context.UserIdentifier;
            var token = GenarateTokenZegoCloud.GenerateToken04(request.Params.AppId, userId, request.Params.Secret,
             request.Params.EffectiveTimeInSeconds, request.Params.Payload);
            if (!request.RoomChat.IsGroup)
            {
                await Task.WhenAll(Clients.Caller.SendAsync("RecievedTokenZegoCloud", token.token),
                _publisher.Publish(new AcceptedRequestCallEvent(Guid.NewGuid(), request.Params, Guid.Parse(userId), request.RoomChat)));
            }
        }
        public async Task RejectStartCall(Domain.Entities.RoomChat roomChat)
        {
            if (roomChat.IsGroup)
            {
                // reject call group
            }
            else
            {
                // reject call private
            }
        }
        public async Task StopCallRequest(StopCallRequest request)
        {
            if (request.RoomChat.IsGroup)
            {
                //await _publisher.Publish(new StopedCallPrivateEvent(Guid.NewGuid(), request.Caller, request.RoomChat.Id, true));
            }
            else
            {

                // 1. updateRedis
                // 2. Notify
                await _publisher.Publish(new StopedCallPrivateEvent(Guid.NewGuid(), request.Caller));
            }
        }
        public async Task RequestOpenVideo()
        {

        }

    }
}
