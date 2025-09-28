
namespace DocuCheck.Domain.Entities.ChecksHistory.Enums;

public record CheckResult(ResultType ResultType, DocumentType Type, string? Message)
{
    public static CheckResult Error(DocumentType type, string message) => new(ResultType.Error, type, message);
    public static CheckResult Valid(DocumentType type) => new(ResultType.Valid, type, null);
    public static CheckResult Invalid(DocumentType type) => new(ResultType.Invalid, type, null);

    public override string ToString()
    {
        return ResultType switch
        {
            ResultType.Valid   => $"Dokument je platný.",
            ResultType.Invalid => $"Dokument nalezen v databázi neplatných dokumentů.",
            ResultType.Error   => Message ?? $"Error při kontrole dokumentu.",
            _ => throw new ArgumentOutOfRangeException(nameof(ResultType))
        };
    }
}

public enum ResultType
{
    Invalid = 0,
    Valid = 1,
    Error = 5
}