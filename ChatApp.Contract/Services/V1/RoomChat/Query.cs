

using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Enumerations;
using static ChatApp.Contract.Services.V1.RoomChat.Respone;

namespace ChatApp.Contract.Services.V1.RoomChat
{
    public static class Query
    {
        public record GetRoomChatsByQuery(string? UserId, string? SearchTerm, string? SortColunm, SortOrder? SortOrder) 
            : IQuery<List<RoomChatRespone>>;
        public record GetRoomChatById(Guid Id) : IQuery<RoomChatRespone>;
    }
}
