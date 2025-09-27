using DocuCheck.Main;
using DocuCheck.Main.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
app.MapEndpoints();
app.Run();
