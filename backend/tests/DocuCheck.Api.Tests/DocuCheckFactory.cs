
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DocuCheck.Api.Tests
{
    internal class DocuCheckFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)  
        {
            builder.ConfigureServices(_ =>
            {
            });
        }
    }
}