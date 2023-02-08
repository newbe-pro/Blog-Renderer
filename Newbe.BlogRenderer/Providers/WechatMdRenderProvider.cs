using Markdig;

namespace Newbe.BlogRenderer.Providers;

public class WechatMdRenderProvider : IMdRenderProvider
{
    public Task<string> RenderAsync(string source)
    {
        var builder = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Use<WechatStyleExtension>()
            .UsePipeTables()
            .UseFootnotes();
        var pipeline = builder.Build();
        var html = Markdown.ToHtml(source, pipeline);
        return Task.FromResult(html);
    }
}