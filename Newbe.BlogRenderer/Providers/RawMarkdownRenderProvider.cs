namespace Newbe.BlogRenderer.Providers;

public abstract class RawMarkdownRenderProvider : IMdRenderProvider
{
    public Task<string> RenderAsync(string source)
    {
        // remove yaml front matter from source
        var index = source.IndexOf(Consts.PlaceHolders.Markdown.YamlFrontMatter,
            StringComparison.InvariantCultureIgnoreCase);
        if (index > 0)
        {
            source = source[(index + Consts.PlaceHolders.Markdown.YamlFrontMatter.Length)..];
        }

        return Task.FromResult(source);
    }
}