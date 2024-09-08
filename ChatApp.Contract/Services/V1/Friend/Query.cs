

using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;

namespace ChatApp.Contract.Services.V1.Friend
{
    public static class Query
    {
       public record GetAllFriendQuery(string UserId) : IQuery<PageResult<UserDTO>>;
    }
}
