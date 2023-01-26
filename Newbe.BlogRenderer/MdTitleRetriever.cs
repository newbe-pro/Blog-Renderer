using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;

namespace Newbe.BlogRenderer;

public class MdTitleRetriever : IMdTitleRetriever
{
    public Task<string?> GetTitleAsync(string markdown)
    {
        var builder = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .UseFootnotes();
        var pipeline = builder.Build();
        var markdownDocument = Markdig.Markdown.Parse(markdown, pipeline);
        foreach (var markdownObject in markdownDocument.Descendants())
        {
            if (markdownObject is YamlFrontMatterBlock block)
            {
                foreach (var stringLine in block.Lines.Lines)
                {
                    var s = stringLine.ToString();
                    if (s.Contains("title:"))
                    {
                        return Task.FromResult<string?>(s.Split(":")[1].Trim());
                    }
                }
            }
        }

        return Task.FromResult<string?>(null);
    }
}