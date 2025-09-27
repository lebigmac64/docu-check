using System.Diagnostics.CodeAnalysis;

namespace DocuCheck.Domain.Entities.DocumentHistory.ValueObjects;

public class DocumentNumber
{
    [SuppressMessage(
        "Design", 
        "CS8618:Non-nullable field must contain a non-null value when exiting constructor." +
        " Consider adding the 'required' modifier or declaring as nullable.",
        Justification = "For EF Core")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private DocumentNumber() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
   
    private DocumentNumber(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static DocumentNumber Create(string value)
    {
        return new DocumentNumber(value);
    }
}