using ChatApp.Domain.Abstractions.Dappers;
using ChatApp.Domain.Entities;
using Dapper;

namespace ChatApp.Infrastructure.Dapper.Repositories
{
    public class RoomChatQueryRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RoomChatQueryRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        //public async Task<List<RoomChatRespone>> GetRoomChatsByUserId(Guid? userId)
        //{
        //    using var connection = _connectionFactory.CreateConnection();
        //    const string sql = @"
        //            WITH UserRooms AS (
        //                SELECT DISTINCT cp.RoomChatId
        //                FROM ConvenstionParticipant cp
        //                WHERE cp.UserId = @UserId )
        //            SELECT 
        //                rc.Id, rc.Name AS RoomChatName, rc.IsGroup,
        //                cp.UserId, cp.RoomChatId, cp.NickName,
        //                au.Id, au.Name AS UserName,au.Avatar,
        //                m.Id , m.Content, m.RoomChatId, m.Type, m.CreatedBy, m.CreatedDate, m.SeenByJson
        //            FROM UserRooms ur
        //            JOIN RoomChat rc ON ur.RoomChatId = rc.Id
        //            JOIN ConvenstionParticipant cp ON rc.Id = cp.RoomChatId
        //            JOIN AppUsers au ON cp.UserId = au.Id
        //            LEFT JOIN Message m ON rc.Id = m.RoomChatId
        //            ORDER BY rc.Id, m.CreatedDate DESC";

        //    var roomChatDictionary = new Dictionary<Guid, RoomChatRespone>();

        //    await connection.QueryAsync<RoomChatRespone, ConversationParticipantRespone, InfoMember, Message, RoomChatRespone>(
        //        sql,
        //        (roomChat, participant, user, message) =>
        //        {
        //            if (!roomChatDictionary.TryGetValue(roomChat.Id, out var roomChatEntry))
        //            {
        //                roomChatEntry = roomChat;
        //                roomChatEntry.ConversationParticipants = new List<ConversationParticipantRespone>();
        //                roomChatEntry.Messages = new List<MessageDTO>();
        //                roomChatDictionary.Add(roomChatEntry.Id, roomChatEntry);
        //            }

        //            if (participant != null && !roomChatEntry.ConversationParticipants.Any(p => p.AppUser.Id == user.Id))
        //            {
        //                participant.AppUser = user;
        //                roomChatEntry.ConversationParticipants.Add(participant);
        //            }

        //            if (message != null && !roomChatEntry.Messages.Any(m => m.Id == message.Id))
        //            {
        //                roomChatEntry.Messages.Add(new MessageDTO
        //                {
        //                    Id = message.Id,
        //                    Content = message.Content,
        //                    RoomChatId = message.RoomChatId,
        //                    CreatedBy = message.CreatedBy,
        //                    Type = message.Type,
        //                    CreatedDate = message.CreatedDate
        //                    //SeenBy = FunctionHelper.DeserializeSeenBy(message.SeenByJson)
        //                });
        //            }
        //            return roomChatEntry;
        //        },
        //        new { UserId = userId },
        //        splitOn: "UserId,Id,Id"
        //    );
        //    return roomChatDictionary.Values.ToList();
        //}
        public async Task<List<RoomChat>?> GetIdRoomChatsByUserId(Guid? Id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var result = await connection.QueryAsync<RoomChat>("GetIdRoomChatByUserId", 
                new { UserId = Id }, 
                commandType: System.Data.CommandType.StoredProcedure);
            return result.ToList();
        }
        //public async Task<List<MessageDTO>?> GetListMessages(Guid roomChatId, int? limit = 10, int? offset = 0)
        //{
        //    using var connection = _connectionFactory.CreateConnection();
        //    var sql = @"Select Id,RoomChatId,Content,Type,CreatedBy,CreatedDate 
        //                From Message Where RoomChatId = @RoomChatId
        //                ORDER BY CreatedDate DESC
        //                OFFSET @Offset ROWS 
        //                FETCH NEXT @Limit ROWS ONLY;";
        //    var result = await connection.QueryAsync<MessageDTO>(sql,
        //        new { RoomChatId = roomChatId, Offset = offset, Limit = limit });
        //    return result.ToList();
        //}
    }
}
