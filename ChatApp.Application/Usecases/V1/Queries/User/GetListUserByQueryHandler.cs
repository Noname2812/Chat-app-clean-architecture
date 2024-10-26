

using AutoMapper;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;
using ChatApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ChatApp.Contract.Services.V1.User.Query;

namespace ChatApp.Application.Usecases.V1.Queries.User
{
    public sealed class GetListUserByQueryHandler : IQueryHandler<SearchListUsersQuery, PageResult<UserDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public GetListUserByQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Result<PageResult<UserDTO>>> Handle(SearchListUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.Where(x =>  x.Email == request.KeySearch
            || (x.Name != null && x.Name.Contains(request.KeySearch))).ToListAsync();
            var usersResult = users.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            var items = _mapper.Map<List<UserDTO>>(usersResult) ?? [];
            var resultPage = PageResult<UserDTO>.Create(items, request.PageIndex, request.PageSize, users.Count());
            return resultPage;
        }
    }
}
