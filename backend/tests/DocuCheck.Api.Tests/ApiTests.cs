using System.Threading.Tasks;

namespace DocuCheck.Api.Tests;

public class ApiTests
{
    [Fact]
    public async Task GetRoot_ReturnsOk()
    {
        using var factory = new DocuCheckFactory();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/");
        response.EnsureSuccessStatusCode();

        var content =  await response.Content.ReadAsStringAsync();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }
}
