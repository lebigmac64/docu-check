using System.Text.Json;
using DocuCheck.Application.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Main.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DocuCheck.Main.Endpoints
{
    public static class Endpoints
    {
        public static void MapEndpoints(this IEndpointRouteBuilder routeBuilder) =>
            routeBuilder.MapDocumentEndpoints();

        private static void MapDocumentEndpoints(this IEndpointRouteBuilder routeBuilder) =>
            routeBuilder.MapGet("api/documents/check/{documentNumber}",
                async
                ([FromRoute] string documentNumber, HttpContext ctx, IDocumentService documentService) =>
                {
                    ctx.Response.ContentType = "text/event-stream";
                    ctx.Response.Headers.CacheControl = "no-cache";

                    var total = Enum.GetValues<DocumentType>().Length;
                    await ctx.Response.WriteAsync($"event: total\ndata: {total}\n\n");
                    await ctx.Response.Body.FlushAsync();

                    await foreach (var result in documentService.CheckDocumentAsync(documentNumber))
                    {
                        var dto = MapCheckResultDocumentCheckResultDto(result);
                        var sseFrame = $"id: {Guid.NewGuid()}\nevent: checkResult\ndata: {JsonSerializer.Serialize(dto)}\n\n";
                        await ctx.Response.WriteAsync(sseFrame);
                        await ctx.Response.Body.FlushAsync();
                    }

                    const string doneFrame = $"event: done\ndata: \"All document types checked.\"\n\n";
                    await ctx.Response.WriteAsync(doneFrame);
                    await ctx.Response.Body.FlushAsync();
                });

        private static DocumentCheckResultDto MapCheckResultDocumentCheckResultDto(CheckResult result)
        {
            var data = new DocumentCheckResultDto(
                ResultType: (byte)result.ResultType,
                Type: (byte)result.Type,
                CheckedAt: DateTime.UtcNow,
                Note: result.ToString(),
                RecordedAt: result.RecordedAtRaw);

            return data;
        }
    }
}