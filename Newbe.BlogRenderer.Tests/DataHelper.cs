namespace Newbe.BlogRenderer.Tests;

public static class DataHelper
{
    public static async Task<string> Get0X01ContentAsync()
    {
        var md = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "data", "0x01.md"));
        return md;
    }
}