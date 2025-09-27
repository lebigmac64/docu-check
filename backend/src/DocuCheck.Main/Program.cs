using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("api/documents/check/{number}", ([FromRoute] string number) =>
{
    Debug.WriteLine(number);

    return Results.NoContent();
})
.WithName("CheckDocumentNumberValid")
.WithOpenApi();

app.Run();
