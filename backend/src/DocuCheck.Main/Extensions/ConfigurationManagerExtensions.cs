namespace DocuCheck.Main.Extensions;

public static class ConfigurationManagerExtensions
{
    public static string GetMinistryApiBaseAddress(this ConfigurationManager configuration)
    {
        var baseAddress = configuration["HttpClients:MinistryApi:BaseAddress"];
        if (string.IsNullOrWhiteSpace(baseAddress))
        {
            throw new ArgumentNullException(nameof(baseAddress), "MinistryApi BaseAddress is not configured.");
        }

        return baseAddress;
    }
        
}