

using Carter;
using ChatApp.Contract.Enumerations;
using ChatApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.RoomChat.Command;
using static ChatApp.Contract.Services.V1.RoomChat.Query;

namespace ChatApp.Presentation.APIs.RoomChats
{
    public class RoomChatApi : ApiEndpoint, ICarterModule
    {
        private const string BaseURL = "/api/v{version:apiVersion}/roomchats";
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("Roomchats")
                .MapGroup(BaseURL).HasApiVersion(1).RequireAuthorization();
            group1.MapGet(string.Empty, GetAllRoomChatByUserV1);
            group1.MapPost(string.Empty, CreateRoomChatV1);
            group1.MapGet("{roomId}", () => "");
            group1.MapDelete("{roomId}", () => "");
            group1.MapPut("{roomId}", () => "");

            // example version 2
            //var group2 = app.NewVersionedApi("roomchats")
            //    .MapGroup(BaseURL).HasApiVersion(2);
            //group2.MapPost(string.Empty, () => "");
        }
        public static async Task<IResult> GetAllRoomChatByUserV1(ISender sender, ClaimsPrincipal user, string? searchTerm = null, string? sortColunm = null, 
            string? sortOrder = null)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var sort = !string.IsNullOrWhiteSpace(sortOrder)
                ? sortOrder.Equals("Asc")
                ? SortOrder.Ascending : SortOrder.Descending
                : SortOrder.Descending; // default descending and descending by createDate

            var result = await sender.Send(new GetRoomChatsByQuery(userId,searchTerm,sortColunm, sort));
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
        public static async Task<IResult> CreateRoomChatV1(ISender sender, [FromBody] CreateRoomChatCommand room)
        {
            var result = await sender.Send(room);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
    }
}
