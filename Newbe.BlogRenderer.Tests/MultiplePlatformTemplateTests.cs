namespace Newbe.BlogRenderer.Tests;

public class MultiplePlatformTemplateTests
{
    [Test]
    public async Task ParseTest()
    {
        var endingTemplates = MultiplePlatformTemplate.Parse(PreDefinedContent.EndingTemplate, null);
        var adTemplates = MultiplePlatformTemplate.Parse(PreDefinedContent.AdTemplate, null);
        await Verify(new { endingTemplates, adTemplates });
    }
}