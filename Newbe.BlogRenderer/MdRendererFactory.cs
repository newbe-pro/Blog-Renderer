using Autofac.Features.Indexed;

namespace Newbe.BlogRenderer;

internal class MdRendererFactory : IMdRendererFactory
{
    private readonly IIndex<RenderPlatform, IMdRenderProvider> _services;

    public MdRendererFactory(
        IIndex<RenderPlatform, IMdRenderProvider> services)
    {
        _services = services;
    }

    public IMdRenderProvider Create(RenderPlatform renderPlatform)
    {
        return _services[renderPlatform];
    }
}