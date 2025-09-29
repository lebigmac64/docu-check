using System.Globalization;
using System.Text.Json;
using DocuCheck.Application.Services.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Main.Contracts.GetDocumentCheckHistory;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using DocumentCheckResultDto = DocuCheck.Main.Contracts.CheckDocument.DocumentCheckResultDto;

namespace DocuCheck.Main.Endpoints.Documents
{
    public static class DocumentEndpoints
    {
        public static void MapDocumentEndpoints(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapGet("api/documents/check/{documentNumber}",
                async
                    ([FromRoute] string documentNumber, 
                        HttpContext ctx, 
                        IDocumentService documentService, 
                        CancellationToken cancellationToken = default) =>
                {
                    ctx.Response.ContentType = "text/event-stream";
                    ctx.Response.Headers.CacheControl = "no-cache";

                    var total = Enum.GetValues<DocumentType>().Length;
                    await ctx.Response.WriteAsync($"event: total\ndata: {total}\n\n", cancellationToken: cancellationToken);
                    await ctx.Response.Body.FlushAsync(cancellationToken);
                    
                    await foreach (var result in documentService.CheckDocumentAsync(documentNumber).WithCancellation(cancellationToken))
                    {
                        var dto = MapCheckResultDocumentCheckResultDto(result);
                        var sseFrame = $"id: {Guid.NewGuid()}\nevent: checkResult\ndata: {JsonSerializer.Serialize(dto)}\n\n";
                        await ctx.Response.WriteAsync(sseFrame, cancellationToken: cancellationToken);
                        await ctx.Response.Body.FlushAsync(cancellationToken);
                    }
                    
                    const string doneFrame = $"event: done\ndata: \"All document types checked.\"\n\n";
                    await ctx.Response.WriteAsync(doneFrame, cancellationToken: cancellationToken);
                    await ctx.Response.Body.FlushAsync(cancellationToken);
                });
            
            routeBuilder.MapGet("api/documents/history",            
                async (IDocumentService documentService, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) =>
                {
                    var history = await documentService.GetDocumentCheckHistoryAsync(pageNumber, pageSize);
                    var dto = history.Select(MapCheckHistoryGetDocumentCheckHistoryDto);
                    
                    return Results.Ok(dto.ToArray());
                });
        }
        
        private static GetDocumentCheckHistoryDto MapCheckHistoryGetDocumentCheckHistoryDto(CheckHistory history)
        {
            var data = new GetDocumentCheckHistoryDto(
                Id: history.Id.ToString(),
                DocumentNumber: history.Number.Value,
                CheckedAt: history.CheckedAt.ToString(CultureInfo.InvariantCulture),
                ResultType: (int)history.ResultType);

            return data;
        }

        private static DocumentCheckResultDto MapCheckResultDocumentCheckResultDto(CheckResult result)
        {
            var data = new DocumentCheckResultDto(
                ResultType: (byte)result.ResultType,
                Type: (byte)result.Type,
                CheckedAt: DateTime.UtcNow,
                RecordedAt: result.RecordedAtRaw);

            return data;
        }
    }
}