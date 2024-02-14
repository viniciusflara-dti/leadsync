using Microsoft.OpenApi.Models;

namespace LeadSync.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "LeadSync API",
                Description = "A simple example ASP.NET Core Web API <br> Environment: " +
                            $"[{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}]" +
                            $"<br> Machine Name: [{Environment.MachineName}]",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Shayne Boyer",
                    Email = string.Empty,
                    Url = new Uri("https://twitter.com/spboyer"),
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://example.com/license"),
                }
            });
        });

        return services;
    }
}