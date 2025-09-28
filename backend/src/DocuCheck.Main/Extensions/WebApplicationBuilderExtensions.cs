using System.Net;
using DocuCheck.Application.Services;
using DocuCheck.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;

namespace DocuCheck.Main.Extensions
{
    internal static class WebApplicationBuilderExtensions
    {
        internal static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
        }
        
        private static void AddApplication(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<DocumentService>();
        }
    }
}