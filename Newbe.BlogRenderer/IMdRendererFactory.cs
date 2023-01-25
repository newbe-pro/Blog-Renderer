namespace Newbe.BlogRenderer;

public interface IMdRendererFactory
{
    IMdRenderProvider Create(RenderPlatform renderPlatform);
}