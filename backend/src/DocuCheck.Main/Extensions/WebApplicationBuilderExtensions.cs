namespace DocuCheck.Main.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder) 
        {
            builder.Services.AddHttpClient();
        }
    }
}