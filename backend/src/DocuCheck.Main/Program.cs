using DocuCheck.Main;
using DocuCheck.Main.Endpoints;
using DocuCheck.Main.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
    
app.ConfigureMiddleware();
    
app.Run();
