using Autofac.Extras.Moq;

namespace Newbe.BlogRenderer.Tests;

public class MdTitleRetrieverTests
{
    [Test]
    public async Task GetTitleTest()
    {
        using var autoMock = AutoMock.GetStrict();
        var mdTitleRetriever = autoMock.Create<MdTitleRetriever>();
        var markdown = await DataHelper.Get0X01ContentAsync();
        var title = await mdTitleRetriever.GetTitleAsync(markdown);
        await Verify(title);
    }
}