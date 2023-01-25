using Autofac.Extras.Moq;

namespace Newbe.BlogRenderer.Tests;

public abstract class MdRenderProviderTestBase<T> where T : IMdRenderProvider
{
    private static async Task<string> Get0X01ContentAsync()
    {
        var md = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "data", "0x01.md"));
        return md;
    }

    [Test]
    public async Task Render0X01MdAsync()
    {
        using var autoMock = AutoMock.GetStrict();
        var mdRenderProvider = autoMock.Create<T>();
        var source = await Get0X01ContentAsync();
        var html = await mdRenderProvider.RenderAsync(source);
        await Verify(html, "html");
    }
}