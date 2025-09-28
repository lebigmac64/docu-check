namespace DocuCheck.Api.Tests.Functional;

public class ApiTests
{
    // [Theory]
    // [InlineData("123456789")]
    // [InlineData("1234567AB")]
    // public async Task PostValidDocument_ReturnsNoContent(string number)
    // {
    //     await using var factory = new DocuCheckFactory();
    //     var client = factory.CreateClient();
    //
    //     var response = await client.PostAsync($"api/documents/check/{number}", new StringContent(""));
    //
    //     response.EnsureSuccessStatusCode();
    //     Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
    // }
    //
    // [Theory]
    // [InlineData("012345678")]
    // [InlineData("AČ123456")]
    // [InlineData("ABC12345")] 
    // [InlineData("00012345")]
    // [InlineData("123456")]
    // [InlineData("ZZ-654321")]
    // public async Task PostInvalidDocumentNumberFormat_ReturnsBadRequest(string number)
    // {
    //     await using var factory = new DocuCheckFactory();
    //     var client = factory.CreateClient();
    //
    //     var response = await client.PostAsync($"api/documents/check/{number}", new StringContent(""));
    //
    //     Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
    // }
}
