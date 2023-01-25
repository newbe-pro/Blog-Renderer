using Markdig;
using Newbe.BlogRenderer.Providers;

namespace Newbe.BlogRenderer.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var md = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, "data", "0x01.md"));
        var builder = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Use<WeChatStyleExtension>()
            .UseFootnotes()
            .UseBootstrap();
        var pipeline = builder.Build();
        var html = Markdig.Markdown.ToHtml(md, pipeline);
        await File.WriteAllTextAsync(Path.Combine(AppContext.BaseDirectory, "0x01.html"), html);
        await Verify(html);
    }
}