

using AutoMapper;
using ChatApp.Contract.Abstractions.Message;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Contract.DTOs;
using ChatApp.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using static ChatApp.Contract.Services.V1.User.Query;
using ChatApp.Domain.Exceptions;

namespace ChatApp.Application.Usecases.V1.Queries.User
{
    public sealed class GetInfoUserByIdQueryHandler : IQueryHandler<GetInfoUserByIdQuery, UserDTO>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public GetInfoUserByIdQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Result<UserDTO>> Handle(GetInfoUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if(user == null)
            {
                throw new IdentityException.UserNotFound(request.UserId);
            }
            var result = _mapper.Map<UserDTO>(user);
            return Result.Success(result);
        }
    }
}
