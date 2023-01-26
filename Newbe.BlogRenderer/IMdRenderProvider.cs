namespace Newbe.BlogRenderer;

public interface IMdRenderProvider
{
    Task<string> RenderAsync(string source);
}