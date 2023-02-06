namespace Newbe.BlogRenderer;

public interface IMdRender
{
    Task<string> Render(MdRenderOptions options);
}

public record MdRenderOptions(RenderPlatform Platform, string Markdown)
{
    public string CopyrightTemplate { get; set; } = null!;
    public string EndingTemplate { get; set; } = null!;
}