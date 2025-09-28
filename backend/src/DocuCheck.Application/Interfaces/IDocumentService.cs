using DocuCheck.Domain.Entities.ChecksHistory.Enums;

namespace DocuCheck.Application.Interfaces;

public interface IDocumentService
{
    IAsyncEnumerable<CheckResult> CheckDocumentAsync(string documentNumber);
}