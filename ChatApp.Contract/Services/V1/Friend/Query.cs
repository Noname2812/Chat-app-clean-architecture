

using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;

namespace ChatApp.Contract.Services.V1.Friend
{
    public static class Query
    {
       public record GetAllFriendQuery(Guid UserId,string? KeySearch, int PageIndex, int PageSize) : IQuery<PageResult<UserDTO>>;
    }
}
