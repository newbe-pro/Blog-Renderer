namespace Newbe.BlogRenderer;

public class MdRender : IMdRender
{
    private readonly IMdRendererFactory _mdRendererFactory;

    public MdRender(
        IMdRendererFactory mdRendererFactory)
    {
        _mdRendererFactory = mdRendererFactory;
    }

    public async Task<string> Render(RenderPlatform renderPlatform, string markdown)
    {
        var mdRender = _mdRendererFactory.Create(renderPlatform);
        var result = await mdRender.RenderAsync(markdown);
        return result;
    }
}