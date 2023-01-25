namespace Newbe.BlogRenderer;

public interface IMdRender
{
    Task<string> Render(RenderPlatform renderPlatform, string markdown);
}