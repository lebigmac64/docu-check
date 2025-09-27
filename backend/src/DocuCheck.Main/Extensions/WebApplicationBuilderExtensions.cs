using DocuCheck.Infrastructure.Persistence;
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
    }
}