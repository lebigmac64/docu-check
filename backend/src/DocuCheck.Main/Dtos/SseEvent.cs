namespace DocuCheck.Main.Dtos;

public record SseEvent(string Id, string Event, object Data);