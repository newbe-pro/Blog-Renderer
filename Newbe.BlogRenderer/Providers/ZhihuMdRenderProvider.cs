using Markdig;

namespace Newbe.BlogRenderer.Providers;

public class ZhihuMdRenderProvider : IMdRenderProvider
{
    public Task<string> RenderAsync(string source)
    {
        var builder = new MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseYamlFrontMatter();
        var pipeline = builder.Build();
        var html = Markdown.ToHtml(source, pipeline);
        return Task.FromResult(html);
    }
}