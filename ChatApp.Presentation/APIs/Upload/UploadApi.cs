

using Carter;
using ChatApp.Domain.Enums;
using ChatApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static ChatApp.Contract.Services.V1.Upload.Command;

namespace ChatApp.Presentation.APIs.Upload
{
    public class UploadApi : ApiEndpoint, ICarterModule
    {
        private const string BaseURL = "/api/v{version:apiVersion}/upload";

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group1 = app.NewVersionedApi("Upload")
                .MapGroup(BaseURL).HasApiVersion(1);
            group1.MapPost(string.Empty, UploadFileV1).DisableAntiforgery();
        }
        public static async Task<IResult> UploadFileV1(ISender sender,IFormFile file, TypeMessage? type )
        {

            var result = await sender.Send(new ParamUploadFileCommand(file, type ?? TypeMessage.String));
            if (result.IsFailure)
            {
                return HandleFailure(result);
            }
            return Results.Ok(result);
        }
    }
}
