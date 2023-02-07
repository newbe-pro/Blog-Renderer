namespace Newbe.BlogRenderer;

public class MdRender : IMdRender
{
    private readonly IMdRendererFactory _mdRendererFactory;
    private readonly IPreRender _preRender;

    public MdRender(
        IMdRendererFactory mdRendererFactory,
        IPreRender preRender)
    {
        _mdRendererFactory = mdRendererFactory;
        _preRender = preRender;
    }

    public async Task<string> Render(MdRenderOptions options)
    {
        var mdRender = _mdRendererFactory.Create(options.Platform);
        var preRenderAsync = await _preRender.PreRenderAsync(options);
        var result = await mdRender.RenderAsync(preRenderAsync);
        return result;
    }
}