

using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.Enumerations;
using static ChatApp.Contract.Services.V1.RoomChat.Respone;

namespace ChatApp.Contract.Services.V1.RoomChat
{
    public static class Query
    {
        public record GetRoomChatsByQuery(string? UserId, string? KeySearch, SortOrder? SortOrder, int PageIndex, int PageSize) 
            : IQuery<PageResult<RoomChatRespone>>;
        public record GetRoomChatById(Guid Id, int? Offset = 0, int? Limit = 10) : IQuery<RoomChatRespone>;
    }
}
