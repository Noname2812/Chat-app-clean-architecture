using ChatApp.API.DependencyInjection.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ChatApp.API.DependencyInjection.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme { Description = "JWT Authorization here !", Name = "Authorization", In = ParameterLocation.Header, Scheme = "Bearer" });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id= "Bearer",
                                Type = ReferenceType.SecurityScheme,
                            },
                            Scheme ="oauth2",
                            Name ="Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigSwaggerOptions>();
        }
        public static void ConfigureSwagger(this WebApplication app)
        {
            app.UseSwagger();
            
            app.UseSwaggerUI(options => 
            {
                foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
                }
                options.DisplayRequestDuration();
                options.EnableTryItOutByDefault();
                options.DocExpansion(DocExpansion.None);
            });
            app.MapGet("/", ()=> Results.Redirect("/swagger/index.html")).WithTags(string.Empty);
        }
    }
}
