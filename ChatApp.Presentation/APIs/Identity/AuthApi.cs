

using Carter;
using ChatApp.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static ChatApp.Contract.Services.V1.Identty.Command;

namespace ChatApp.Presentation.APIs.Identity;

public class AuthApi : ApiEndpoint, ICarterModule
{
    private const string BaseURL = "/api/v{version:apiVersion}/auth";
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group1 = app.NewVersionedApi("Authentication")
                .MapGroup(BaseURL).HasApiVersion(1);
        group1.MapPost("login", LoginV1);
        group1.MapPost("refresh-token", RefreshTokenV1);
        group1.MapPost("register", RegisterAccountV1);
        group1.MapPost("logout", LogoutV1).RequireAuthorization();

    }
    public static async Task<IResult> LoginV1(ISender sender, [FromBody] Contract.Services.V1.Identty.Query.LoginQuery login)
    {
        var result = await sender.Send(login);
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        return Results.Ok(result);
    }
    public static async Task<IResult> RefreshTokenV1(ISender sender, [FromBody] Contract.Services.V1.Identty.Query.RefreshTokenQuery tokens)
    {
        var result = await sender.Send(tokens);
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        return Results.Ok(result);
    }
    public static async Task<IResult> RegisterAccountV1(ISender sender, [FromBody] RegisterAccountCommand account)
    {
        var result = await sender.Send(account);
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        return Results.Ok(result);
    }
    public static async Task<IResult> LogoutV1(ISender sender, HttpContext httpContext)
    {
        var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var result = await sender.Send(new LogoutCommand(token));
        if (result.IsFailure)
        {
            return HandleFailure(result);
        }
        return Results.Ok(result);
    }
}
