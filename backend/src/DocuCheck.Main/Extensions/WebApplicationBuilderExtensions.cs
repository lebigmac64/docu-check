using System.Net;
using DocuCheck.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace DocuCheck.Main.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder) 
        {
            builder.AddPersistence();
            builder.AddInfrastructure();
        }
        
        private static void AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient("MinistryApi", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration.GetMinistryApiBaseAddress());
            });
        }
        
        private static void AddPersistence(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration
                .GetConnectionString(DocuCheckDbContext.ConnectionStringKey);
            builder.Services.AddDbContext<DocuCheckDbContext>(options =>
                options.UseSqlite(connectionString));

            using var scope = builder.Services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DocuCheckDbContext>();
            dbContext.Database.EnsureCreated();
            if (dbContext.Database.HasPendingModelChanges())
            {
                dbContext.Database.Migrate();
            }
        }
        
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/problem+json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                            title = "An error occurred while processing your request.",
                            status = context.Response.StatusCode,
                            detail = contextFeature.Error.Message
                        });
                    }
                });
            });
        }
    }
}