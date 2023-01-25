using Autofac;
using Newbe.BlogRenderer.Providers;

namespace Newbe.BlogRenderer.Modules;

public class BlogRendererModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterTypes(
                typeof(MdRendererFactory),
                typeof(MdRender))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterModule<WechatModule>();
    }
}

public class WechatModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<WechatMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Wechat);
    }
}