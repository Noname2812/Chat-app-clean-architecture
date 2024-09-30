
using Carter;
using ChatApp.Contract.Abstractions.Shared;
using ChatApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.User.Command;
using static ChatApp.Contract.Services.V1.User.Query;

namespace ChatApp.Presentation.APIs.User
{
    public  class UserApi : ApiEndpoint, ICarterModule
    {
        private const string BaseURL = "/api/v{version:apiVersion}/user";
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("User")
              .MapGroup(BaseURL).HasApiVersion(1).RequireAuthorization();
            group1.MapGet("/vertify/{userId}", GetCodeVertifyAccountV1);
            group1.MapPost("/vertify/{userId}", VertifyAccountV1);
            group1.MapPut("/change-info", UpdateInfoUserV1);
            group1.MapGet("{userId}", GetInfoUserV1).AllowAnonymous();
            group1.MapGet(string.Empty, GetListUsersByQueryV1).AllowAnonymous();
        }
        public static async Task<IResult> GetCodeVertifyAccountV1(ISender sender,  Guid userId)
        {
            return Results.Ok();
        }
        public static async Task<IResult> GetInfoUserV1(ISender sender, Guid userId)
        {
            var query = new GetInfoUserByIdQuery(userId);
            var result = await sender.Send(query);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
        public static async Task<IResult> VertifyAccountV1(ISender sender, Guid userId)
        {
            return Results.Ok();
        }
        public static async Task<IResult> UpdateInfoUserV1(ISender sender, ClaimsPrincipal userClaimsPrincipal, [FromBody] UpadteUserCommand user)
        {
            var userId = userClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var userUpdate = new UpadteUserCommand(Guid.Parse(userId), user.CurrentPassword, user.NewPassword, user.Name, user.Avatar, user.DayOfBirth);
            var result = await sender.Send(userUpdate);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
        public static async Task<IResult> GetListUsersByQueryV1(ISender sender, string? KeySearch, int? PageIndex, int? PageSize)
        {
            var result = await sender.Send(new SearchListUsersQuery(KeySearch,PageIndex ?? 1,PageSize ?? 10));
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);

        }

    }
}
