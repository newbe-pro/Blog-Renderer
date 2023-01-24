using Markdig;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace Newbe.BlogRenderer;

public static class Consts
{
    public const string DataTool = "Newbe.BlogRenderer";
}

public static class HtmlAttributesExtensions
{
    public static HtmlAttributes AddDataTool(this HtmlAttributes htmlAttributes)
    {
        htmlAttributes.AddPropertyIfNotExist("data-tool", Consts.DataTool);
        return htmlAttributes;
    }

    public static HtmlAttributes AddStyle(this HtmlAttributes htmlAttributes, string style)
    {
        htmlAttributes.AddPropertyIfNotExist("style", style);
        return htmlAttributes;
    }
}

public class WeChatStyleExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        pipeline.DocumentProcessed -= PipelineOnDocumentProcessed;
        pipeline.DocumentProcessed += PipelineOnDocumentProcessed;
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
    }

    private static void PipelineOnDocumentProcessed(MarkdownDocument document)
    {
        foreach (var descendant in document.Descendants())
        {
            switch (descendant)
            {
                case HeadingBlock headingBlock:
                {
                    var headingAttribute = headingBlock.GetAttributes();
                    headingAttribute
                        .AddDataTool()
                        .AddStyle(
                            "margin-top: 30px; margin-bottom: 15px; padding: 0px; font-weight: bold; color: black; border-bottom: 2px solid rgb(239, 112, 96); font-size: 1.3em;");
                    if (headingBlock.Inline?.FirstChild is LiteralInline literalInline)
                    {
                        var htmlInline = new HtmlInline(
                            $"<span class=\"prefix\" style=\"display: none;\"></span><span class=\"content\" style=\"display: inline-block; font-weight: bold; background: rgb(239, 112, 96); color: #ffffff; padding: 3px 10px 1px; border-top-right-radius: 3px; border-top-left-radius: 3px; margin-right: 3px;\">{literalInline.Content}</span><span class=\"suffix\"></span><span style=\"display: inline-block; vertical-align: bottom; border-bottom: 36px solid #efebe9; border-right: 20px solid transparent;\"> </span>");
                        headingBlock.Inline.Clear();
                        headingBlock.Inline.AppendChild(htmlInline);
                    }
                }
                    break;
                case ParagraphBlock paragraphBlock:
                {
                    var htmlAttributes = paragraphBlock.GetAttributes();
                    htmlAttributes
                        .AddDataTool()
                        .AddStyle(
                            "font-size: 16px; padding-top: 8px; padding-bottom: 8px; margin: 0; line-height: 26px; color: black;");
                }
                    break;
                case ListBlock listBlock:
                {
                    listBlock.GetAttributes()
                        .AddDataTool()
                        .AddStyle(
                            "margin-top: 8px; margin-bottom: 8px; padding-left: 25px; color: black; list-style-type: disc;");
                    foreach (var listItemBlock in listBlock.Descendants<ListItemBlock>())
                    {
                        listItemBlock.GetAttributes()
                            .AddDataTool()
                            .AddProperty("style",
                                "margin-top: 5px; margin-bottom: 5px; line-height: 26px; text-align: left; color: rgb(1,1,1); font-weight: 500;");
                        foreach (var linkInline in listItemBlock.Descendants<LinkInline>())
                        {
                            linkInline.GetAttributes()
                                .AddDataTool()
                                .AddStyle(
                                    "text-decoration: none; word-wrap: break-word; font-weight: bold; color: rgb(239, 112, 96); border-bottom: 1px solid rgb(239, 112, 96);");
                        }
                    }
                }
                    break;
            }
        }
    }
}