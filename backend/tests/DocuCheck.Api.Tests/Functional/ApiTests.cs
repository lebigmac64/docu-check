namespace DocuCheck.Api.Tests.Functional;

public class ApiTests
{
    [Theory]
    [InlineData("123456789")]
    [InlineData("1234567AB")]
    public async Task PostValidDocument_ReturnsValidEventStream(string number)
    {
        await using var factory = new DocuCheckFactory();
        var client = factory.CreateClient();

        var response = await client.PostAsync($"api/documents/check/{number}", new StringContent(""));
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);
        var events = new List<string>();
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (line != null && line.StartsWith("id: "))
            {
                events.Add(line);
            }
        }

        Assert.NotEmpty(events);
    }
}
