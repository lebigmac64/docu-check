namespace DocuCheck.Main.Dtos;

public record DocumentCheckResultDto(byte ResultType, byte Type, DateTime CheckedAt, string? Note, string RecordedAt);