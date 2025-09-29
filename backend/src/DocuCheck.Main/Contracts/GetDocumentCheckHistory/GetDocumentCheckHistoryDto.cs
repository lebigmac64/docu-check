namespace DocuCheck.Main.Contracts.GetDocumentCheckHistory;

public record GetDocumentCheckHistoryDto(
    string Id,
    string CheckedAt,
    string DocumentNumber,
    int ResultType);