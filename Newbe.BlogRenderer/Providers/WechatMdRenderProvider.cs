using Markdig;

namespace Newbe.BlogRenderer.Providers;

public class WechatMdRenderProvider : IMdRenderProvider
{
    public Task<string> RenderAsync(string source)
    {
        var builder = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Use<WechatStyleExtension>()
            .UseFootnotes();
        var pipeline = builder.Build();
        var html = Markdig.Markdown.ToHtml(source, pipeline);
        return Task.FromResult(html);
    }
}