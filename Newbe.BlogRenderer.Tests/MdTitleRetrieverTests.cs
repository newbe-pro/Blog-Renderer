using Autofac.Extras.Moq;

namespace Newbe.BlogRenderer.Tests;

public class MdTitleRetrieverTests
{
    [Test]
    public async Task GetTitleTest()
    {
        using var autoMock = AutoMock.GetStrict();
        var mdTitleRetriever = autoMock.Create<YamlFrontMatterRetriever>();
        var markdown = await DataHelper.Get0X01ContentAsync();
        var title = await mdTitleRetriever.GetYamlFrontAsync(markdown);
        await Verify(title);
    }
}