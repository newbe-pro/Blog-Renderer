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
                typeof(MdRender),
                typeof(PreRender),
                typeof(YamlFrontMatterRetriever))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterModule<WechatModule>();
        builder.RegisterModule<CnblogsModule>();
        builder.RegisterModule<ZhihuModule>();
        builder.RegisterModule<TencentCloudModule>();
        builder.RegisterModule<ToutiaoModule>();
        builder.RegisterModule<InfoQModule>();
        builder.RegisterModule<BilibiliQModule>();
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

public class CnblogsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<CnblogsMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Cnblogs);
    }
}

public class ZhihuModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<ZhihuMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Zhihu);
    }
}

public class TencentCloudModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<TencentCloudMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.TencentCloud);
    }
}

public class ToutiaoModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<ToutiaoMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Toutiao);
    }
}

public class InfoQModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<InfoQMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.InfoQ);
    }
}

public class BilibiliQModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<BilibiliMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Bilibili);
    }
}

