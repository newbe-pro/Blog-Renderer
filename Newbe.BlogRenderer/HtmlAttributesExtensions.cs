using Markdig.Renderers.Html;

namespace Newbe.BlogRenderer;

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