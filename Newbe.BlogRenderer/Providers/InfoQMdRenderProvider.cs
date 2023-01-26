using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using CodeBlockRenderer = Markdig.Renderers.Html.CodeBlockRenderer;

namespace Newbe.BlogRenderer.Providers;

public class InfoQMdRenderProvider : IMdRenderProvider
{
    public async Task<string> RenderAsync(string source)
    {
        // var builder = new MarkdownPipelineBuilder()
        //     .UseYamlFrontMatter()
        //     .Use<InfoQExtension>();
        var builder = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter();
        var pipeline = builder.Build();
        var markdownDocument = MarkdownParser.Parse(source, pipeline, null);
        await using var stringWriter = new StringWriter();
        var htmlRenderer = new HtmlRenderer(stringWriter)
        {
            EnableHtmlForBlock = false
        };
        htmlRenderer.Render(markdownDocument);
        await stringWriter.FlushAsync();
        var html = stringWriter.ToString();
        return html;
    }
}

public class InfoQExtension : IMarkdownExtension
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
        textRendererBase.ObjectRenderers.Add(new RawCodeBlockRenderer(exact));
    }
}

public class RawCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
    private readonly CodeBlockRenderer _codeBlockRenderer;

    public RawCodeBlockRenderer(CodeBlockRenderer codeBlockRenderer)
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
                if (renderer.EnableHtmlForBlock)
                {
                    renderer.Write("<pre");
                    renderer.Write("><code");
                    renderer.Write(">");
                    renderer.Write($"```{fencedCodeBlock?.Info}");
                    renderer.WriteLine();
                }

                renderer.WriteLeafRawLines(obj, true, true);
                if (renderer.EnableHtmlForBlock)
                    renderer.WriteLine("```</code></pre>");
                renderer.EnsureLine();
            }
        }
    }
}