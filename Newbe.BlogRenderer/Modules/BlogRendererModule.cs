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
        builder.RegisterModule<BilibiliModule>();
        builder.RegisterModule<JuejinModule>();
        builder.RegisterModule<CsdnModule>();
        builder.RegisterModule<AliyunModule>();
        builder.RegisterModule<SifouModule>();
    }
}

public sealed class WechatModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<WechatMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Wechat);
    }
}

public sealed class CnblogsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<CnblogsMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Cnblogs);
    }
}

public sealed class ZhihuModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<ZhihuMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Zhihu);
    }
}

public sealed class TencentCloudModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<TencentCloudMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.TencentCloud);
    }
}

public sealed class ToutiaoModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<ToutiaoMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Toutiao);
    }
}

public sealed class InfoQModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<InfoQMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.InfoQ);
    }
}

public sealed class BilibiliModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<BilibiliMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Bilibili);
    }
}

public sealed class JuejinModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<JuejinMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Juejin);
    }
}

public sealed class CsdnModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<CsdnMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Csdn);
    }
}

public sealed class AliyunModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<AliyunMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Aliyun);
    }
}

public sealed class SifouModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);
        builder.RegisterType<SifouMdRenderProvider>()
            .Keyed<IMdRenderProvider>(RenderPlatform.Sifou);
    }
}