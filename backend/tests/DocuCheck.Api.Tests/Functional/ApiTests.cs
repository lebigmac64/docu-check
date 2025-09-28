using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace DocuCheck.Api.Tests.Functional;

public class ApiTests
{
    [Theory]
    [InlineData("123456789")]
    [InlineData("1234567AB")]
    public async Task PostValidDocumentNumber_ReturnsNoContent(string number)
    {
        await using var factory = new DocuCheckFactory();
        var client = factory.CreateClient();

        var response = await client.PostAsync($"api/documents/check/{number}", new StringContent(""));

        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    }
}
