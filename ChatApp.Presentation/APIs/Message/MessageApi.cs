using Carter;
using ChatApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using static ChatApp.Contract.Services.V1.Message.Command;
using static ChatApp.Contract.Services.V1.Message.Query;
using static ChatApp.Contract.Services.V1.RoomChat.Command;
using static ChatApp.Contract.Services.V1.RoomChat.Query;


namespace ChatApp.Presentation.APIs.Message
{
    public class MessageApi : ApiEndpoint, ICarterModule
    {
        private const string BaseURL = "/api/v{version:apiVersion}/message";

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("Message")
                .MapGroup(BaseURL).HasApiVersion(1).RequireAuthorization();
            //group1.MapPost(string.Empty, CreateMessageV1);
            group1.MapGet(string.Empty, GetMessagesByRoomIdV1);
            group1.MapDelete("{MessageId}", () => "");
            group1.MapPut("{MessageId}", () => "");
        }
        public static async Task<IResult> CreateMessageV1(ISender sender, [FromBody] CreateMessageCommand message)
        {
            var result = await sender.Send(message);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
        public static async Task<IResult> GetMessagesByRoomIdV1(ISender sender,Guid RoomId, int PageIndex=1, int PageSize=10)
        {
            var query = new GetMessagesByRoomIdQuery(RoomId,PageSize,PageIndex);
            var result = await sender.Send(query);
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
    }
}
