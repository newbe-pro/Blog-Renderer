using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;

namespace Newbe.BlogRenderer;

public class YamlFrontMatterRetriever : IYamlFrontMatterRetriever
{
    public Task<YamlFront> GetYamlFrontAsync(string markdown)
    {
        var builder = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .UseFootnotes();
        var pipeline = builder.Build();
        var markdownDocument = Markdig.Markdown.Parse(markdown, pipeline);
        var yamlFront = new YamlFront();
        foreach (var markdownObject in markdownDocument.Descendants())
        {
            if (markdownObject is YamlFrontMatterBlock block)
            {
                foreach (var stringLine in block.Lines.Lines)
                {
                    var s = stringLine.ToString();
                    if (s.Contains("title:"))
                    {
                        yamlFront.Title = s.Replace("title:", "").Trim();
                    }
                    else if (s.Contains("summary:"))
                    {
                        yamlFront.Summary = s.Replace("summary:", "").Trim();
                    }
                    else if (s.Contains("cover:"))
                    {
                        yamlFront.Cover = s.Replace("cover:", "").Trim();
                    }
                    else if (s.Contains("slag:"))
                    {
                        yamlFront.Slag = s.Replace("slag:", "").Trim();
                    }
                    else if (s.Contains("tags:"))
                    {
                        yamlFront.Tags = s.Replace("tags:", "")
                            .Trim('[', ']', ' ');
                    }
                }
            }
        }

        return Task.FromResult(yamlFront);
    }
}