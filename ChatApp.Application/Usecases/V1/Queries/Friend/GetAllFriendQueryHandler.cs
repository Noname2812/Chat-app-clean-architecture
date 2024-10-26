

using AutoMapper;
using ChatApp.Application.Abstractions;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;
using ChatApp.Domain.Abstractions.Repositories;
using ChatApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using static ChatApp.Contract.Services.V1.Friend.Query;

namespace ChatApp.Application.Usecases.V1.Queries.Friend
{
    public sealed class GetAllFriendQueryHandler : BaseQueryHandler<Domain.Entities.Friend, Guid>, IQueryHandler<GetAllFriendQuery, PageResult<UserDTO>>
    {
        public GetAllFriendQueryHandler(IRepositoryBase<Domain.Entities.Friend, Guid> repository, IMapper mapper) : base(repository, mapper)
        {

        }
        public async Task<Result<PageResult<UserDTO>>> Handle(GetAllFriendQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.FindAll(f =>
               (f.UserId == request.UserId || f.FriendId == request.UserId) &&
               f.Status == StatusFriend.Accepted);

            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                query = query.Where(f =>
                    (f.UserId == request.UserId &&
                        (EF.Functions.Like(f.UserFriend.Name, $"%{request.KeySearch}%") ||
                         EF.Functions.Like(f.UserFriend.Email, $"{request.KeySearch}"))) ||
                    (f.FriendId == request.UserId &&
                        (EF.Functions.Like(f.UserOf.Name, $"%{request.KeySearch}%") ||
                         EF.Functions.Like(f.UserOf.Email, $"{request.KeySearch}")))
                );
            }
            var totalCount = await query.CountAsync(cancellationToken);
            var friends = await query
                .Select(f => f.UserId == request.UserId ? f.UserFriend : f.UserOf)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);
            var result = _mapper.Map<List<UserDTO>>(friends ?? []);
            var pageResult = PageResult<UserDTO>.Create(result ?? [], request.PageIndex, request.PageSize, totalCount);
            return Result.Success(pageResult);
        }
    }
}
