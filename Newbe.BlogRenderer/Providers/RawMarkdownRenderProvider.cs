namespace Newbe.BlogRenderer.Providers;

public abstract class RawMarkdownRenderProvider : IMdRenderProvider
{
    public Task<string> RenderAsync(string source)
    {
        // remove yaml front matter from source
        var lastIndexOf = source.LastIndexOf("---", StringComparison.OrdinalIgnoreCase);
        if (lastIndexOf > 0)
        {
            source = source[(lastIndexOf + 3)..];
        }

        return Task.FromResult(source);
    }
}