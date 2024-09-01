using ChatApp.Infrastructure.DependencyInjection.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ChatApp.API.DependencyInjection.Extensions
{
    public static class JWTExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
                {
                    JWTOptions jwtOptions = new JWTOptions();
                    configuration.GetSection(nameof(JWTOptions)).Bind(jwtOptions);
                    var key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
                    // storing the JWT in the AuthenticationProperties allows you to retrieve it from elsewhere withinyour application
                    // example var  accesstoken = await HttpContext.GetTokenAsync(access_token);
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false, // on production is true
                        ValidateIssuer = false, // on production is true
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero,
                    };
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Append("IS-TOKEN-EXPRIED", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();
        }
    }
}
