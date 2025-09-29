using DocuCheck.Application.Interfaces;
using DocuCheck.Infrastructure.Clients.MinistryOfInterior;
using DocuCheck.Infrastructure.Extensions;
using DocuCheck.Infrastructure.Persistence;
using DocuCheck.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DocuCheck.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddPersistence(configuration);
        services.ConfigureHttpClients(configuration);

        services.AddScoped<IMinistryOfInteriorService, MinistryOfInteriorService>();
    }
    
    private static void ConfigureHttpClients(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddHttpClient<MinistryOfInteriorClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetMinistryApiBaseAddress());
        });
    }
    
    private static void AddPersistence(this IServiceCollection services, IConfigurationManager configuration)
    {
        var connectionString = configuration
            .GetConnectionString(DocuCheckDbContext.ConnectionStringKey);
        services.AddDbContext<DocuCheckDbContext>(options =>
            options.UseSqlite(connectionString));

        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DocuCheckDbContext>();
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
    }
}