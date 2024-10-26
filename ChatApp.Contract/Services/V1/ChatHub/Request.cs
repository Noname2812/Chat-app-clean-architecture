

using ChatApp.Contract.Services.V1.ChatHub.Common;
using ChatApp.Domain.Enums;

namespace ChatApp.Contract.Services.V1.ChatHub
{
    public static class Request
    {
        public record CallRequest(Guid Caller,Domain.Entities.RoomChat RoomChat,  TypeCall Type);
        public record AcceptCallRequest(ParamsCreateTokenZegoCloud Params, Domain.Entities.RoomChat RoomChat);
        public record StopCallRequest(Guid Caller, Domain.Entities.RoomChat RoomChat);
    }
}
