using System.Diagnostics.CodeAnalysis;
using DocuCheck.Domain.Entities.DocumentHistory.Enums;
using DocuCheck.Domain.Entities.DocumentHistory.ValueObjects;

namespace DocuCheck.Domain.Entities.DocumentHistory
{
    
    public class DocumentHistory
    {
        [SuppressMessage(
            "Design", 
            "CS8618:Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.", 
            Justification = "For EF Core")]
        #pragma warning disable CS8618
        private DocumentHistory() { }
        #pragma warning restore CS8618

        private DocumentHistory(
            Guid id,
            DateTime checkedAt,
            DocumentNumber number,
            CheckResult validationResult)
        {
            Id = id;
            CheckedAt = checkedAt;
            Number = number;
            ValidationResult = validationResult;
        }

        public Guid Id { get; private set; }
        
        public DateTime CheckedAt { get; private set; }
        
        public DocumentNumber Number { get; private set; }

        public CheckResult ValidationResult { get; private set; }

        public static DocumentHistory Create(
            DateTime checkedAt, 
            DocumentNumber number,
            CheckResult checkResult)
        {
            return new DocumentHistory(
                Guid.NewGuid(), 
                checkedAt, 
                number, 
                checkResult);
        }
    }
}