using Markdig;

namespace Newbe.BlogRenderer.Providers;

public class TencentCloudMdRenderProvider : IMdRenderProvider
{
    public Task<string> RenderAsync(string source)
    {
        var builder = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .UseFootnotes();
        var pipeline = builder.Build();
        var html = Markdig.Markdown.ToHtml(source, pipeline);
        return Task.FromResult(html);
    }
}