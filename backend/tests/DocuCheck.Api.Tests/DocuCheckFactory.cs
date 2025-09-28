using DocuCheck.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DocuCheck.Api.Tests
{
    internal class DocuCheckFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)  
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<DocuCheckDbContext>();
                services.AddDbContext<DocuCheckDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                });
            });
            
            base.ConfigureWebHost(builder);
        }
    }
}