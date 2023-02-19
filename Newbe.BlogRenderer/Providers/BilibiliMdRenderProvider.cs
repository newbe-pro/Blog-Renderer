using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Newbe.BlogRenderer.Providers;

public class BilibiliMdRenderProvider : IMdRenderProvider
{
    public Task<string> RenderAsync(string source)
    {
        var builder = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Use<BilibiliExtension>()
            .UsePipeTables()
            .UseFootnotes();
        var pipeline = builder.Build();
        var html = Markdig.Markdown.ToHtml(source, pipeline);
        return Task.FromResult(html);
    }
}

public class BilibiliExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer == null)
            throw new ArgumentNullException(nameof(renderer));
        if (renderer is not TextRendererBase<HtmlRenderer> textRendererBase)
            return;
        var exact = textRendererBase.ObjectRenderers.FindExact<CodeBlockRenderer>();
        if (exact == null)
        {
            return;
        }

        textRendererBase.ObjectRenderers.Remove(exact);
        textRendererBase.ObjectRenderers.Add(new BilibiliCodeBlockRenderer(exact));
    }
}

public class BilibiliCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
    private readonly CodeBlockRenderer _codeBlockRenderer;

    public BilibiliCodeBlockRenderer(CodeBlockRenderer codeBlockRenderer)
    {
        _codeBlockRenderer = codeBlockRenderer;
    }

    protected override void Write(HtmlRenderer renderer, CodeBlock obj)
    {
        if (obj is not FencedCodeBlock fencedCodeBlock || obj.Parser is not FencedCodeBlockParser parser)
        {
            _codeBlockRenderer.Write(renderer, obj);
        }
        else
        {
            var str = fencedCodeBlock.Info?.Replace(parser.InfoPrefix!, string.Empty);
            if (string.IsNullOrWhiteSpace(str))
            {
                _codeBlockRenderer.Write(renderer, obj);
            }
            else
            {
                renderer.EnsureLine();
                // failed to copy and paste code block to bilibili even if I use highlight the code by prism.
                // renderer.Write("<pre");
                // renderer.Write(" data-lang=\"");
                // renderer.Write("text/");
                // renderer.Write(str);
                // renderer.Write("\" codecontent=\"");
                // renderer.WriteLeafRawLines(obj, true, true);
                // renderer.Write("\" class=\"");
                // renderer.Write(" language-");
                // renderer.Write(str);
                // renderer.Write("\"");
                // renderer.Write("><code>");
                // renderer.WriteLeafRawLines(obj, true, true);
                // renderer.Write("</code></pre>");
                renderer.Write(
                    "<blockquote>\n<p>Bilibili 代码块无法正常渲染，因此无法正常显示。请关注微信公众号“newbe技术专栏”，搜索对应文章代码内容。\n<img src=\"https://www.newbe.pro/images/weixin_public_qrcode.png\" alt=\"关注微信公众号“newbe技术专栏”\"></p>\n</blockquote>");
                renderer.EnsureLine();
            }
        }
    }
}