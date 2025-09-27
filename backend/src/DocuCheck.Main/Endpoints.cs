using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DocuCheck.Main
{
    public static class Endpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("api/documents/check/{number}", ([FromRoute] string number) =>
            {
                Debug.WriteLine(number);

                return Results.NoContent();
            });
        }
    }
}