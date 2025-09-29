using DocuCheck.Application.Repositories.Interfaces;
using DocuCheck.Application.Services.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;

namespace DocuCheck.Application.Services;

internal class DocumentService(
    IMinistryOfInteriorService ministryOfInteriorService, 
    ICheckHistoryRepository checkHistoryRepository) 
    : IDocumentService
{
    public async IAsyncEnumerable<CheckResult> CheckDocumentAsync(string documentNumber)
    {
        var docNumber = DocumentNumber.Create(documentNumber);

        var results = new List<CheckResult>();

        try
        {
            foreach (var documentType in Enum.GetValues<DocumentType>())
            {
                var checkResult = await ministryOfInteriorService.CheckDocumentValidityAsync(docNumber, documentType);
                results.Add(checkResult);
                yield return checkResult;
            }
        }
        finally
        {
            if (results.Count > 0)
            {
                var finalCheckResult = results.Any(r => r.ResultType == ResultType.Invalid)
                    ? ResultType.Invalid
                    : ResultType.Valid;

                var historyRecord = CheckHistory.Create(
                    DateTime.UtcNow,
                    docNumber,
                    finalCheckResult);

                await checkHistoryRepository.AddAsync(historyRecord);
            }
        }
    }

    public async Task<CheckHistory[]> GetDocumentCheckHistoryAsync(int pageNumber, int pageSize)
    {
        var document = await checkHistoryRepository.GetCheckHistoryAsync(pageNumber, pageSize);
        
        return document;
    }
}