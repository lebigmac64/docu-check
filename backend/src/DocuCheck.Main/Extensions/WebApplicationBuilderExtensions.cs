using DocuCheck.Application;
using DocuCheck.Application.Interfaces;
using DocuCheck.Infrastructure;
using DocuCheck.Main.Providers;

namespace DocuCheck.Main.Extensions
{
    internal static class WebApplicationBuilderExtensions
    {
        internal static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IEnvironmentProvider>(services =>
            {
                var env = services.GetRequiredService<IWebHostEnvironment>();
                return new HostEnvironmentProvider(env.EnvironmentName);
            });
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
        }
    }
}