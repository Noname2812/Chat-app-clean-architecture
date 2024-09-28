

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
            group1.MapGet(string.Empty, GetRoomChatsByUserV1);
            group1.MapPost(string.Empty, CreateRoomChatV1);
            group1.MapGet("{RoomId}", GetRoomChatByIdV1);
            group1.MapDelete("{RoomId}", () => "");
            group1.MapPut("{RoomId}", () => "");

            // example version 2
            //var group2 = app.NewVersionedApi("roomchats")
            //    .MapGroup(BaseURL).HasApiVersion(2);
            //group2.MapPost(string.Empty, () => "");
        }
        public static async Task<IResult> GetRoomChatsByUserV1(ISender sender, ClaimsPrincipal user, 
            string? searchTerm = null, string? sortColunm = null,string? sortOrder = null,
            int pageIndex = 0, int pageSize = 10)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var sort = !string.IsNullOrWhiteSpace(sortOrder)
                ? sortOrder.Equals("Asc")
                ? SortOrder.Ascending : SortOrder.Descending
                : SortOrder.Descending; // default descending and descending by createDate

            var result = await sender.Send(new GetRoomChatsByQuery(userId, searchTerm, sortColunm, sort, pageIndex, pageSize));
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
        public static async Task<IResult> GetRoomChatByIdV1(ISender sender, Guid RoomId, int? offset = 0, int? limit = 10)
        {
            var result = await sender.Send(new GetRoomChatById(RoomId,offset,limit));
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
    }
}
