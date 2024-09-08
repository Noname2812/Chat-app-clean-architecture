using Carter;
using ChatApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.Friend.Command;
using static ChatApp.Contract.Services.V1.Friend.Query;

namespace ChatApp.Presentation.APIs.Friend
{
    public class FriendApi : ApiEndpoint, ICarterModule
    {
        private const string BaseURL = "/api/v{version:apiVersion}/friends";
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("Friend")
                .MapGroup(BaseURL).HasApiVersion(1).RequireAuthorization();
            group1.MapGet(string.Empty, GetAllFriendsV1);
            group1.MapPost("add-friend", SendRequestAddFriendV1);
            group1.MapPost("update-request-add-friend", UpdateRequestAddFriendV1);
        }
        public static async  Task<IResult> GetAllFriendsV1(ISender sender, ClaimsPrincipal user) 
        {

            var result = await sender.Send(new GetAllFriendQuery(user.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
        public static async  Task<IResult> SendRequestAddFriendV1(ISender sender, ClaimsPrincipal user, [FromBody] CreateRequestAddFriendCommand req) 
        {
            var result = await sender.Send(new CreateRequestAddFriendCommand(Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)),req.To));
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
        public static async  Task<IResult> UpdateRequestAddFriendV1(ISender sender, ClaimsPrincipal user, [FromBody] UpdateStatusFriendCommand req) 
        {
            var request = new UpdateStatusFriendCommand(Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)), req.To, req.Status);
            var result = await sender.Send(request);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
    }
}
