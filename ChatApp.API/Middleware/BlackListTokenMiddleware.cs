
using ChatApp.Application.Abstractions.Services;
using System.Text.Json;

namespace ChatApp.API.Middleware
{
    public class BlackListTokenMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (token != null)
            {
                var cacheService = context.RequestServices.GetRequiredService<IRedisService>();
                var cacheRespone = await cacheService.GetDataByKey($"black-list-token:{token}");
                if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrEmpty(cacheRespone))
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new { Message = "Token has been blacklisted" }));
                    return;
                }
            }
            await next(context);
        }
    }
}
