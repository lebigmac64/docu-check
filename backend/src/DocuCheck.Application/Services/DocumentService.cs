using DocuCheck.Application.Interfaces;
using DocuCheck.Domain.Entities.ChecksHistory.Enums;
using DocuCheck.Domain.Entities.ChecksHistory.ValueObjects;

namespace DocuCheck.Application.Services;

internal class DocumentService(IMinistryOfInteriorService ministryOfInteriorService) : IDocumentService
{
    public async IAsyncEnumerable<CheckResult> CheckDocumentAsync(string documentNumber)
    {
        var docNumber = DocumentNumber.Create(documentNumber);
        
        foreach (var documentType in Enum.GetValues<DocumentType>())
        {
            yield return await ministryOfInteriorService.CheckDocumentValidityAsync(docNumber, documentType);
        }
    }
}