using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;

namespace ChatApp.Contract.Services.V1.User
{
    public static class Query
    {
        public record GetInfoUserByIdQuery(Guid UserId) : IQuery<UserDTO>;
        public record SearchListUsersQuery(string? KeySearch, int PageIndex, int PageSize) : IQuery<PageResult<UserDTO>>;
    }
}
